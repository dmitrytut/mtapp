using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MtApp.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{

		bool isBusy;
		public bool IsBusy
		{
			get
			{
				return isBusy;
			}
			set
			{
				isBusy = value;
				RaisePropertyChanged();
			}
		}

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;
		protected void RaisePropertyChanged([CallerMemberName]  string propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

	}
}
