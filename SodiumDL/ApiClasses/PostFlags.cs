using System.Text.Json.Serialization;

namespace SodiumDL.ApiClasses
{
	/// <summary>
	///     represents a posts status flags
	/// </summary>
	public class PostFlags
	{
		/// <summary>
		///     indicates whether the post is pending administrator approval
		/// </summary>
		[JsonPropertyName("pending")]
		public bool Pending { get; set; }

		/// <summary>
		///     indicates whether the post is flagged for deletion
		/// </summary>
		[JsonPropertyName("flagged")]
		public bool Flagged { get; set; }

		/// <summary>
		///     indicates whether the posts notes are locked
		/// </summary>
		[JsonPropertyName("note_locked")]
		public bool NoteLocked { get; set; }

		/// <summary>
		///     indicates whether the posts status is locked
		/// </summary>
		[JsonPropertyName("status_locked")]
		public bool StatusLocked { get; set; }

		/// <summary>
		///     indicates whether the posts rating is locked
		/// </summary>
		[JsonPropertyName("rating_locked")]
		public bool RatingLocked { get; set; }

		[JsonPropertyName("deleted")] public bool Deleted { get; set; }
	}
}