using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SodiumDL.JsonConverter
{
	public class FileConverter : JsonConverter<Uri>
	{
		public override Uri Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			string url = null;
			while (reader.Read())
			{
				// url not found
				if (reader.TokenType == JsonTokenType.EndObject)
					break;

				if (reader.TokenType != JsonTokenType.PropertyName || reader.GetString() != "url") continue;

				reader.Read();
				url = reader.GetString();
			}

			return url == null ? null : new Uri(url);
		}

		public override void Write(Utf8JsonWriter writer, Uri value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
}