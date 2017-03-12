using System;
using SQLite;

namespace LightSwitch
{
	public class LightBulb
	{
		public LightBulb() { }

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public override bool Equals(object obj)
		{
			var leftBulb = this;
			var rightBulb = (LightBulb)obj;

			return leftBulb.ID.Equals(rightBulb.ID);
		}

		public override int GetHashCode()
		{
			return ID;
		}
	}
}
