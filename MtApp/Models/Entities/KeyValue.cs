using System;
using MtApp.Data;
using SQLite;

namespace MtApp.Models.Entities
{
	[Table("KvStorage")]
	public class KeyValue : BaseEntity
	{
		public string Key { get; set; }
		public string Value { get; set; }
	}
}