using System;
using System.IO;
using MtApp.Data;
using MtApp.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(Sqlite))]
namespace MtApp.Droid
{
	public class Sqlite : ISqlite
	{
		public Sqlite() { }
		public string GetDatabasePath(string sqliteFilename)
		{
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var path = Path.Combine(documentsPath, sqliteFilename);
			return path;
		}
	}
}

