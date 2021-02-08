namespace SodiumDL.JsonConverter
{
	public class TagConverter : JsonConverter<PostTag[]>
	{
		public override PostTag[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var allTags = new List<PostTag>();
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
						allTags.Add(new PostTag {Value = tag, Group = ParseGroup(group)});
						break;
				}

			return allTags.ToArray();
		}

		public override void Write(Utf8JsonWriter writer, PostTag[] value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}

		private static TagGroup ParseGroup(string groupName)
		{
			return (TagGroup) Enum.Parse(typeof(TagGroup), groupName, true);
		}
	}
}