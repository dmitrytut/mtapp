using System;
using MtApp.IoC;
using Ninject;
using Xamarin.Forms;

namespace MtApp
{
	public partial class App : Application
	{
		static IKernel _kernel;
		public static IKernel Kernel => _kernel ?? new StandardKernel(new MtAppDefaultModule());

		public App()
		{
			MainPage = new SearchView();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
