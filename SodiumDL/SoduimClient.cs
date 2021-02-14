using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using SodiumDL.ApiClasses;

namespace SodiumDL
{
	public class SodiumClient
	{
		private const string E621BaseUri = "https://e621.net/posts.json";
		private const string E926BaseUri = "https://e926.net/posts.json";

		private readonly LimitedHttpClient _httpClient;
		private readonly bool _useSafeMode;

		public SodiumClient(bool useSafeMode = false, string userAgent = null)
		{
			_useSafeMode = useSafeMode;
			userAgent ??= $"SodiumDL/{Assembly.GetExecutingAssembly().GetName().Version} (by d3r_5h06un)";

			_httpClient = new LimitedHttpClient(1, userAgent);
		}

		public async Task<IEnumerable<Post>> GetPostsAsync(string tagQuery, int postLimit, bool includeDeleted = false)
		{
			var posts = new List<Post>();
			var retries = 5;
			for (var i = 0; i < postLimit;)
			{
				// get page
				string response;
				if (posts.Count == 0)
					response = await _httpClient.LimitedGetString(BuildRequestUri(tagQuery, postLimit - i));
				else
					response = await _httpClient.LimitedGetString(
						BuildRequestUri(tagQuery, postLimit - i, posts[^1].Id));

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

				posts.AddRange(newPosts.Where(post => !post.Flags.Deleted || includeDeleted));
				i += newPosts.Count;
			}

			return posts;
		}

		private Uri BuildRequestUri(string tagQuery = default, int postLimit = default,
			ulong beforePost = default)
		{
			var uriBuilder = new UriBuilder(_useSafeMode ? E926BaseUri : E621BaseUri);

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