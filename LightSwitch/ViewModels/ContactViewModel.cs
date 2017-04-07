using System;
namespace LightSwitch
{
	public class ContactViewModel : BaseViewModel
	{
		private Contact contact;
		public Contact Contact
		{
			get { return contact; }
			set
			{
				contact = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		public string Name
		{
			get { return contact.Name; }
			set
			{
				contact.Name = value;
				OnPropertyChanged();
			}
		}

		public ContactViewModel() 
		{
			contact = new Contact();
		} 

		public override bool Equals(object obj)
		{
			return contact.Equals(((ContactViewModel)obj).contact);
		}

		public override int GetHashCode()
		{
			return contact.GetHashCode();
		}
	}
}
