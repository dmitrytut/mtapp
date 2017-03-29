using System;
using Xamarin.Forms;
using MtApp.Data;
using MtApp.iOS;
using System.IO;

[assembly: Dependency(typeof(Sqlite))]
namespace MtApp.iOS
{
	public class Sqlite : ISqlite
	{
		public Sqlite() { }
		public string GetDatabasePath(string sqliteFilename)
		{
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var libraryPath = Path.Combine(documentsPath, "..", "Library");
			var path = Path.Combine(libraryPath, sqliteFilename);

			return path;
		}
	}
}
