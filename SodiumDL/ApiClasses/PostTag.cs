namespace SodiumDL.ApiClasses
{
	public enum TagGroup
	{
		General,
		Species,
		Character,
		Copyright,
		Artist,
		Invalid,
		Lore,
		Meta
	}

	/// <summary>
	///     represents a single e621 tag
	///     each tag has a category (eg. artist, species, general, ...) and the tag name itself
	/// </summary>
	public class PostTag
	{
		public TagGroup Group { get; set; }
		public string Value { get; set; }

		public override string ToString() => $"({Group}) {Value}";
	}
}