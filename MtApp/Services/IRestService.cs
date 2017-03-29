using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MtApp.Models;

namespace MtApp.Services
{
	public interface IRestService
	{
		Task<IEnumerable<SearchResult>> Search(string query, CancellationToken cancellationToken);

	}
}
