using System;
using System.Collections.Generic;

namespace LightSwitch
{
	public class LightBulb
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public List<Message> Messages { get; set; } = new List<Message>();
		public List<Contact> Contacts { get; set; } = new List<Contact>();
		public List<Quote> Quotes { get; set; } = new List<Quote>();

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
