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

	public struct Tag
	{
		public TagGroup Group { get; set; }
		public string Value { get; set; }
	}
}