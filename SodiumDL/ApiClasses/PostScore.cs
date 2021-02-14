using System.Text.Json.Serialization;

namespace SodiumDL.ApiClasses
{
	public class PostScore
	{
		[JsonPropertyName("up")] public int Up { get; set; }
		[JsonPropertyName("down")] public int Down { get; set; }
		[JsonPropertyName("total")] public int Total { get; set; }

		public override string ToString() => Total.ToString();
	}
}