using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MtApp.Models;
using MtApp.Models.Entities;

namespace MtApp
{
	public interface IKvService
	{
		Task<IEnumerable<SearchResult>> GetLastSearchResults(CancellationToken ct);
		Task SaveLastSearchResults(IEnumerable<SearchResult> results, CancellationToken ct);
	}
}
