using System.Text.Json.Serialization;

namespace SodiumDL.ApiClasses
{
	/// <summary>
	///     represents a posts voting score
	///     votes are divided into up- and down-votes
	///     a combined value is also available
	/// </summary>
	public class PostScore
	{
		[JsonPropertyName("up")] public int Up { get; set; }
		[JsonPropertyName("down")] public int Down { get; set; }
		[JsonPropertyName("total")] public int Total { get; set; }

		public override string ToString() => Total.ToString();
	}
}