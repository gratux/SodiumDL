using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using SodiumDL.ApiClasses;

namespace SodiumDL.JsonConverter
{
	public class TagConverter : JsonConverter<Tag[]>
	{
		public override Tag[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var allTags = new List<Tag>();
			var tagsDone = false;
			var group = "general";
			while (!tagsDone && reader.Read())
				// try
				// {
				// 	Console.Write($"{reader.TokenType}:");
				// 	Console.WriteLine($" {reader.GetString()}");
				// }
				// catch
				// {
				//
				// }

				switch (reader.TokenType)
				{
					case JsonTokenType.EndObject:
						tagsDone = true;
						break;
					case JsonTokenType.PropertyName:
						group = reader.GetString();
						break;
					case JsonTokenType.String:
						var tag = reader.GetString();
						allTags.Add(new Tag {Value = tag, Group = ParseGroup(group)});
						break;
				}

			return allTags.ToArray();
		}

		public override void Write(Utf8JsonWriter writer, Tag[] value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}

		private static TagGroup ParseGroup(string groupName)
		{
			return (TagGroup) Enum.Parse(typeof(TagGroup), groupName, true);
		}
	}
}