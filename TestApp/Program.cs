using System;
using System.Threading.Tasks;
using SodiumDL;

namespace TestApp1
{
	internal static class Program
	{
		private static async Task Main()
		{
			var client = new SodiumClient();
			var posts = client.GetPostsAsync("cheese_grater", 150);
			await foreach (var post in posts)
				Console.WriteLine(post.File.Url?.AbsoluteUri);
		}
	}
}