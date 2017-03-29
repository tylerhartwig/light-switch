using System;
namespace LightSwitch
{
	public class Contact : IComparable<Contact>
	{
		public int ID { get; set; } = -1;
		public string Name { get; set; } = string.Empty;

		public Contact()
		{
		}

		public Contact(ContactDO contactDO)
		{
			ID = contactDO.ID;
			Name = contactDO.Name;
		}

		public override bool Equals(object obj)
		{
			return CompareTo((Contact)obj) == 0;
		}

		public int CompareTo(Contact other)
		{
			var idVal = ID.CompareTo(other.ID);
			var nameVal = Name.CompareTo(other.Name);

			if (idVal == 0)
			{
				return nameVal;
			}

			return idVal;
		}
	}
}
