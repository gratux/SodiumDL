using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using SodiumDL.ApiClasses;

namespace SodiumDL.JsonConverter
{
	public class PostRatingConverter : JsonConverter<PostRating>
	{
		public override PostRating Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var value = reader.GetString();
			return value switch
			{
				"s" => PostRating.Safe,
				"q" => PostRating.Questionable,
				"e" => PostRating.Explicit,
				_ => throw new ArgumentOutOfRangeException(value)
			};
		}

		public override void Write(Utf8JsonWriter writer, PostRating value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
}