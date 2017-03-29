using System;
using SQLite;

namespace LightSwitch
{
	public class LightBulbQuoteDO
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public int LightBulbDO { get; set; }
		public int QuoteDO { get; set; }

		public LightBulbQuoteDO()
		{
		}
	}
}
