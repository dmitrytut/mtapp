using System;
using System.Diagnostics;
using MtApp.Data;
using MtApp.Models.Entities;
using SQLite;

namespace MtApp.Repos
{
	/// <summary>
	/// Key-Value хранилище.
	/// </summary>
	public class KvStorage : Repository<KeyValue>
	{
		public KvStorage(SQLiteAsyncConnection db) : base(db) 
		{
			db.CreateTableAsync<KeyValue>().ContinueWith(t =>
			{
				Debug.WriteLine("KeyValue table initialized!");
			}).Wait();
		}
	}
}
