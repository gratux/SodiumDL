using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using SodiumDL.ApiClasses;

namespace SodiumDL.JsonConverter
{
	public class RatingConverter : JsonConverter<Rating>
	{
		public override Rating Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var value = reader.GetString();
			return value switch
			{
				"s" => Rating.Safe,
				"q" => Rating.Questionable,
				"e" => Rating.Explicit,
				_ => throw new ArgumentOutOfRangeException(value)
			};
		}

		public override void Write(Utf8JsonWriter writer, Rating value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
}