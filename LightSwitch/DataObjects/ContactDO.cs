using System;
namespace LightSwitch
{
	public class ContactDO
	{
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
