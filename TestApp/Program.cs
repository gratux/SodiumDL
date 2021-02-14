using System;
using System.Threading.Tasks;
using SodiumDL;

namespace TestApp1
{
	internal static class Program
	{
		private static async Task Main()
		{
			Console.WriteLine("Hello World!");
			var client = new SodiumClient();
			var posts = await client.GetPostsAsync("cheese_grater", 150);
			foreach (var post in posts) Console.WriteLine(post.File.Url?.AbsoluteUri);
		}
	}
}