using System;
using System.Collections.Generic;
using MtApp.ViewModels;
using Xamarin.Forms;

namespace MtApp
{
	public partial class SearchView : ContentPage
	{
		public SearchView()
		{
			InitializeComponent();

			BindingContext = new SearchViewModel();
		}
	}
}
