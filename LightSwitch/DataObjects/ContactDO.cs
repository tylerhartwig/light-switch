using System;
using SQLite;

namespace LightSwitch
{
	public class ContactDO
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name { get; set; }

		public ContactDO()
		{
		}

		public ContactDO(Contact contact)
		{
			ID = contact.ID;
			Name = contact.Name;
		}
	}
}
