using System;
using SQLite;

namespace LightSwitch
{
	public class LightBulb
	{
		public LightBulb() { }

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
	}
}
