using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MtApp
{
	public static class ObservableCollectionExtensions
	{
		public static void FromEnumeration<T>(this ObservableCollection<T> observableCollection, 
		                                      IEnumerable<T> enumerableList, bool append = false)
		{
			if (observableCollection != null)
			{
				if (!append) observableCollection.Clear();

				foreach (var item in enumerableList)
				{
					observableCollection.Add(item);
				}
			}
		}
	}
}
