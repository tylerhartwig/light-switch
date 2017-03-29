using System;
using SQLite;

namespace LightSwitch
{
	public class LightBulbDO
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name { get; set; } = string.Empty;

		public LightBulbDO() { }

		public LightBulbDO(LightBulb lightBulb)
		{
			ID = lightBulb.ID;
			Name = lightBulb.Name;
		}

		public override bool Equals(object obj)
		{
			var leftBulb = this;
			var rightBulb = (LightBulbDO)obj;
			return leftBulb.ID.Equals(rightBulb.ID) &&
						   leftBulb.Name.Equals(rightBulb.Name);
		}

		public override int GetHashCode()
		{
			return ID;
		}
	}
}
