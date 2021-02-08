using System.Text.Json.Serialization;

namespace SodiumDL.ApiClasses
{
	public struct PostFlags
	{
		[JsonPropertyName("pending")] public bool Pending { get; set; }
		[JsonPropertyName("flagged")] public bool Flagged { get; set; }
		[JsonPropertyName("note_locked")] public bool NoteLocked { get; set; }
		[JsonPropertyName("status_locked")] public bool StatusLocked { get; set; }
		[JsonPropertyName("rating_locked")] public bool RatingLocked { get; set; }
		[JsonPropertyName("delleted")] public bool Deleted { get; set; }
	}
}