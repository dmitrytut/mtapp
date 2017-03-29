using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MtApp.Data;
using MtApp.Models;
using MtApp.Models.Entities;
using Newtonsoft.Json;

namespace MtApp
{
	public class KvService : IKvService
	{
		// Для простоты объявляем статическим полем.
		private string LastSearchResultKey => "lastSearchResult";

		IRepository<KeyValue> KvStorage;
		public KvService(IRepository<KeyValue> kvStorage)
		{
			KvStorage = kvStorage;
		}

		/// <summary>
		/// Получаем результаты последнего запроса из БД.
		/// </summary>
		/// <returns>Результаты последнего запроса.</returns>
		/// <param name="ct"><see cref="CancellationToken"/>.</param>
		public async Task<IEnumerable<SearchResult>> GetLastSearchResults(CancellationToken ct)
		{
			ct.ThrowIfCancellationRequested();

			IEnumerable<SearchResult> result = null;
			await ExecuteOnLatestSearchResult((kv) =>
			{
				if (!string.IsNullOrWhiteSpace(kv?.Value))
				{
					result = JsonConvert.DeserializeObject<IEnumerable<SearchResult>>(kv.Value);
				}
			}).ConfigureAwait(false);

			return result;
		}

		/// <summary>
		/// Сохраняем результат запроса в БД.
		/// </summary>
		/// <param name="results">Список результатов типа <see cref="SearchResult"/>.</param>
		/// <param name="ct"><see cref="CancellationToken"/>.</param>
		public async Task SaveLastSearchResults(IEnumerable<SearchResult> results, CancellationToken ct)
		{
			ct.ThrowIfCancellationRequested();

			await ExecuteOnLatestSearchResult(async (kv) =>
			{
				if (kv != null)
				{
					// Exists
					kv.Value = JsonConvert.SerializeObject(results);
					await KvStorage.Update(kv);
				}
				else
				{
					// New
					await KvStorage.Insert(
						new KeyValue()
						{
							Key = LastSearchResultKey,
							Value = JsonConvert.SerializeObject(results)
						}
					);
				}
			}).ConfigureAwait(false);
		}


		#region Private helpers

		private async Task ExecuteOnLatestSearchResult(Action<KeyValue> fetchedFunc)
		{
			var lastSearchResultKv = await KvStorage.Get(x => x.Key.ToLower() == LastSearchResultKey.ToLower())
													.ConfigureAwait(false);

			fetchedFunc?.Invoke(lastSearchResultKv);
		}

		#endregion
	}
}
