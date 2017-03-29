using System;
namespace MtApp.Data
{
	public interface ISqlite
	{
		string GetDatabasePath(string filename);
	}
}
