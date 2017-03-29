using System;
using SQLite;

namespace MtApp.Data
{
	public abstract class BaseEntity
	{
		[PrimaryKey, Column("Id")]
		public int Id { get; set; }
	}
}
