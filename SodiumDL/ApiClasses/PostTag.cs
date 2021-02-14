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

	public class PostTag
	{
		public TagGroup Group { get; set; }
		public string Value { get; set; }

		public override string ToString() => $"({Group}) {Value}";
	}
}