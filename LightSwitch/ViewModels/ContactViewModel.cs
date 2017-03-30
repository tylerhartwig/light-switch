using System;
namespace LightSwitch
{
	public class ContactViewModel : BaseViewModel
	{
		private Contact contact;

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

		public ContactViewModel(Contact contact)
		{
			this.contact = contact;
		}	
	}
}
