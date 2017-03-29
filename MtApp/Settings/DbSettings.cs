using System;
using MtApp.Data;
using SQLite;
using Xamarin.Forms;

namespace MtApp.Settings
{
	public class DbSettings : IDbSettings
	{
		ISqlite Sqlite;
		public DbSettings()
		{
			Sqlite = DependencyService.Get<ISqlite>();
		}

		public string Name => "mtapp.db";
		public string Path => Sqlite.GetDatabasePath(Name);
		public SQLiteAsyncConnection Connection => new SQLiteAsyncConnection(Path);
	}
}
