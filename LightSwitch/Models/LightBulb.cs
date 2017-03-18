using System;
namespace LightSwitch
{
	public class LightBulb
	{
		public int ID { get; set; }
		public string Name { get; set; }

		public LightBulb()
		{
		}

		public LightBulb(LightBulbDO lightBulbDO)
		{
			ID = lightBulbDO.ID;
			Name = lightBulbDO.Name;
		}
	}
}
