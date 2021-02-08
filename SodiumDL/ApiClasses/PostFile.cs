using System;
using System.Text.Json.Serialization;

namespace SodiumDL.ApiClasses
{
	public class PostFile
	{
		[JsonPropertyName("width")] public int Width { get; set; }
		[JsonPropertyName("height")] public int Height { get; set; }
		[JsonPropertyName("ext")] public string Type { get; set; }
		[JsonPropertyName("size")] public ulong Size { get; set; }
		[JsonPropertyName("md5")] public string MD5 { get; set; }
		[JsonPropertyName("url")] public Uri Url { get; set; }
	}
}