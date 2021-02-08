using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using SodiumDL.JsonConverter;

namespace SodiumDL.ApiClasses
{
	[JsonConverter(typeof(RatingConverter))]
	public enum Rating
	{
		Safe,
		Questionable,
		Explicit
	}

	public class Post
	{
		[JsonPropertyName("id")] public ulong Id { get; set; }
		[JsonPropertyName("score")] public Score Score { get; set; }

		[JsonPropertyName("tags")]
		[JsonConverter(typeof(TagConverter))]
		public Tag[] Tags { get; set; }

		[JsonPropertyName("rating")] public Rating Rating { get; set; }

		[JsonPropertyName("file")]
		[JsonConverter(typeof(FileConverter))]
		public Uri File { get; set; }

		public override string ToString()
		{
			return $"Post (Id: {Id}, Score: {Score})";
		}
	}

	public class ApiResponse
	{
		[JsonPropertyName("posts")] public List<Post> Posts { get; set; }
	}
}