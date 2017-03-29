using System;
using SQLite;

namespace LightSwitch
{
	public class LightBulbMessageDO
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public int LightBulbDO { get; set; }
		public int MessageDO { get; set; }

		public LightBulbMessageDO()
		{
		}
	}
}
