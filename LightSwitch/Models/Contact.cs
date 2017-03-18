using System;
namespace LightSwitch
{
	public class Contact
	{
		public int ID { get; set; }
		public string Name { get; set; }

		public Contact()
		{
		}

		public Contact(ContactDO contactDO)
		{
			ID = contactDO.ID;
			Name = contactDO.Name;
		}
	}
}
