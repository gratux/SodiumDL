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

	public struct PostTag
	{
		public TagGroup Group { get; set; }
		public string Value { get; set; }

		public override string ToString()
		{
			return $"({Group}) {Value}";
		}
	}
}