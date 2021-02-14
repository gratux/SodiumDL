using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SodiumDL
{
	public class LimitedHttpClient
	{
		private readonly HttpClient _client;
		private readonly TimeSpan _minimumRequestTime;
		private DateTime _lastRequest;

		public LimitedHttpClient(double requestsPerSecond, string userAgent)
		{
			_client = new HttpClient();
			_client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);

			_minimumRequestTime = TimeSpan.FromMilliseconds(1000d / requestsPerSecond);
			_lastRequest = DateTime.UnixEpoch;
		}

		public Task<string> LimitedGetString(Uri requestUrl)
		{
			WaitRateLimit();
			return _client.GetStringAsync(requestUrl);
		}

		private void WaitRateLimit()
		{
			var timeout = _minimumRequestTime - (DateTime.UtcNow - _lastRequest);
			if (timeout.TotalMilliseconds > 0)
			{
				Console.WriteLine($"rate limit: waiting for {timeout}");
				Task.Delay(timeout).Wait();
			}

			_lastRequest = DateTime.UtcNow;
		}
	}
}