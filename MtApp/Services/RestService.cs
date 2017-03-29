using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MtApp.Exceptions;
using MtApp.Models;
using MtApp.Settings;
using Newtonsoft.Json;

namespace MtApp.Services
{
	// Для простоты используем обычный HttpClient с дефолтными насройками.
	public class RestService : IRestService
	{
		IApiSettings ApiSettings;
		HttpClient client;

		public RestService(IApiSettings apiSettings)
		{
			ApiSettings = apiSettings;
			client = new HttpClient();
		}

		public async Task<IEnumerable<SearchResult>> Search(string query, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var uri = new Uri($"{ApiSettings.BaseUrl.TrimEnd('/')}/values?searchfield={WebUtility.UrlEncode(query)}");

  			var response = await client.GetAsync(uri);
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<List<SearchResult>>(content);
			}

			throw new NetworkException("Cannot get search results. See http status code inside.", response.StatusCode);
		}
	}
}
