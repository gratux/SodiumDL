using System.Collections.Generic;
using System.Text.Json.Serialization;
using SodiumDL.JsonConverter;

namespace SodiumDL.ApiClasses
{
	[JsonConverter(typeof(PostRatingConverter))]
	public enum PostRating
	{
		Safe,
		Questionable,
		Explicit
	}

	public class Post
	{
		[JsonPropertyName("id")] public ulong Id { get; set; }
		[JsonPropertyName("score")] public PostScore Score { get; set; }

		[JsonPropertyName("tags")]
		[JsonConverter(typeof(TagConverter))]
		public PostTag[] Tags { get; set; }

		[JsonPropertyName("rating")] public PostRating Rating { get; set; }

		[JsonPropertyName("file")] public PostFile File { get; set; }

		[JsonPropertyName("flags")] public PostFlags Flags { get; set; }

		public override string ToString() => $"Post (Id: {Id}, Score: {Score})";
	}

	public class ApiResponse
	{
		[JsonPropertyName("posts")] public List<Post> Posts { get; set; }
	}
}