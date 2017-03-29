using System;
using SQLite;

namespace LightSwitch
{
	public class LightBulbContactDO
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public int LightBulbDO { get; set; }
		public int ContactDO { get; set; }

		public LightBulbContactDO()
		{
		}
	}
}
