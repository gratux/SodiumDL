using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using SodiumDL.ApiClasses;

namespace SodiumDL
{
	public class SodiumClient
	{
		private const string E621BaseUri = "https://e621.net/posts.json";
		private const string E926BaseUri = "https://e926.net/posts.json";

		private readonly LimitedHttpClient _httpClient;

		/// <summary>
		/// create a new SodiumClient instance
		/// </summary>
		/// <param name="safeMode">true when using e926.net (SFW), false when using e621.net (NSFW)</param>
		/// <param name="userAgent">a custom user agent to be used when making web requests</param>
		public SodiumClient(bool safeMode = false, string userAgent = null)
		{
			SafeMode = safeMode;
			userAgent ??= $"{Assembly.GetExecutingAssembly().GetName().Name}/{Assembly.GetExecutingAssembly().GetName().Version} (by d3r_5h06un)";

			_httpClient = new LimitedHttpClient(1, userAgent);
		}

		/// <summary>
		/// true when using e926.net (SFW), false for e621.net (NSFW)
		/// </summary>
		public bool SafeMode { get; set; }

		/// <summary>
		/// gets a number of posts with the specified tags
		/// </summary>
		/// <param name="tagQuery">the space-separated tags</param>
		/// <param name="postLimit">the maximum number of posts returned</param>
		/// <param name="includeDeleted">true if deleted posts should be included</param>
		/// <returns>the found posts</returns>
		public async IAsyncEnumerable<Post> GetPostsAsync(string tagQuery, int postLimit, bool includeDeleted = false)
		{
			ulong lastPostId = 0;
			var retries = 5;
			for (var i = 0; i < postLimit;)
			{
				// get page
				string response;
				if (lastPostId == 0)
					response = await _httpClient.LimitedGetString(BuildRequestUri(tagQuery, postLimit - i));
				else
					response = await _httpClient.LimitedGetString(
						BuildRequestUri(tagQuery, postLimit - i, lastPostId));

				// deserialize to post array
				var responseDeserialized = JsonSerializer.Deserialize<ApiResponse>(response);
				var newPosts = responseDeserialized?.Posts;

				// no or invalid response
				if (newPosts == null)
				{
					if (--retries == 0)
						break;
					continue;
				}

				// no more posts found
				if (newPosts.Count == 0) break;

				var validPosts = newPosts.Where(post => !post.Flags.Deleted || includeDeleted).ToList();

				//lazily return new posts
				foreach (var post in validPosts)
					yield return post;

				lastPostId = newPosts.Last().Id;
				i += validPosts.Count;
			}
		}

		private Uri BuildRequestUri(string tagQuery = default, int postLimit = default,
			ulong beforePost = default)
		{
			var uriBuilder = new UriBuilder(SafeMode ? E926BaseUri : E621BaseUri);

			var query = string.Empty;
			if (tagQuery != default)
				AppendQuery(ref query, $"tags={tagQuery}");
			if (postLimit != default)
				AppendQuery(ref query, $"limit={postLimit}");
			if (beforePost != default)
				AppendQuery(ref query, $"page=b{beforePost}");

			uriBuilder.Query = query;
			return uriBuilder.Uri;
		}

		private static void AppendQuery(ref string uri, string queryToAppend)
		{
			if (uri.Length > 1)
				uri = uri + "&" + queryToAppend;
			else
				uri = queryToAppend;
		}
	}
}