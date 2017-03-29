using System;
using SQLite;

namespace MtApp.Settings
{
	public interface IDbSettings
	{
		string Name { get; }
		string Path { get; }
		SQLiteAsyncConnection Connection { get; }
	}
}
