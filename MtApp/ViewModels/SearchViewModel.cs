using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using MtApp.Data;
using MtApp.Exceptions;
using MtApp.Models;
using MtApp.Models.Entities;
using MtApp.Services;
using Newtonsoft.Json;
using Ninject;
using Xamarin.Forms;

namespace MtApp.ViewModels
{
	public class SearchViewModel : BaseViewModel
	{
		#region Ctor

		public SearchViewModel()
		{
			App.Kernel.Inject(this);

			Init();
		}

		public void Init()
		{
			try
			{
				// Получаем результаты последнего поиска.
				var lastSearchResults = KvService.GetLastSearchResults(cts.Token).Result;
				if (lastSearchResults != null)
				{
					Results = new ObservableCollection<SearchResult>(lastSearchResults);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Ошибка при получении последних результатов поиска: {ex.Message}");
			}
		}

		#endregion

		#region Fields & Properties

		[Inject]
		public IRestService MtApi {get; set;}
		[Inject]
		public IKvService KvService { get; set; }

		private CancellationTokenSource cts = new CancellationTokenSource();

		private string _searchQuery;
		public string SearchQuery
		{
			get { return _searchQuery; }
			set { _searchQuery = value; RaisePropertyChanged(); }
		}

		private ObservableCollection<SearchResult> _results;
		public ObservableCollection<SearchResult> Results
		{
			get { return _results; }
			private set { _results = value; RaisePropertyChanged(); }
		}

		#endregion

		#region Commands

		ICommand searchCommand;
		public ICommand SearchCommand
		{
			get { return searchCommand ?? (searchCommand = new Command(async () => await DoSearch())); }
		}

		#endregion

		#region Handlers

		async Task DoSearch()
		{
			if (string.IsNullOrWhiteSpace(SearchQuery)) return;

			IsBusy = true;

			try
			{
				var searchResults = await MtApi.Search(SearchQuery, cts.Token);
				Results = new ObservableCollection<SearchResult>(searchResults);

				// Добавляем в БД результат. 
				// Вообще, можно сохранять не здесь, а отлавливать завершение приложение и добавлять там, 
				// тогда не надо будет записывать каждый раз.
				await KvService.SaveLastSearchResults(searchResults, cts.Token);
			}
			catch (NetworkException nex)
			{
				Debug.WriteLine($"Сервер вернул результат с кодом: {nex.StatusCode}");
			}
			catch (WebException wex)
			{
				Debug.WriteLine($"Ошибка при подключении к серверу.");
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
			finally
			{
				IsBusy = false;
			}

		}

		#endregion
	}
}
