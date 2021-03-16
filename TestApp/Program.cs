using System;
using System.Threading.Tasks;
using SodiumDL;

namespace TestApp1
{
	internal static class Program
	{
		private static async Task Main()
		{
			var client = new SodiumClient(true);
			var posts = client.GetPostsAsync("on_glass fav:d3r_5h06un", 150);
			await foreach (var post in posts)
				Console.WriteLine(post.File.Url?.AbsoluteUri);
		}
	}
}