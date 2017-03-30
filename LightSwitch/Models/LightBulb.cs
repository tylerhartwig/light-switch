using System;
using System.Collections.Generic;
using System.Linq;

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

		public override int GetHashCode()
		{
			return ID;
		}

		public override bool Equals(object obj)
		{
			var rightLightBulb = (LightBulb)obj;
			return ID == rightLightBulb.ID &&
									   Name == rightLightBulb.Name &&
									   compareIEnumerable(Contacts, rightLightBulb.Contacts) &&
									   compareIEnumerable(Messages, rightLightBulb.Messages) &&
									   compareIEnumerable(Quotes, rightLightBulb.Quotes);
		}

		private Boolean compareIEnumerable<T>(IEnumerable<T> left, IEnumerable<T> right)
		{
			var leftList = left.OrderBy(s => s).ToList();
			var rightList = right.OrderBy(s => s).ToList();

			return Enumerable.SequenceEqual(leftList, rightList);
		}
	}
}
