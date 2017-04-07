using System;
using System.Collections.ObjectModel;

namespace LightSwitch
{
	public class AddContactViewModel : BaseViewModel
	{
		private ObservableCollection<ContactViewModel> contacts;
		public ObservableCollection<ContactViewModel> Contacts
		{
			get { return contacts; }
			set
			{
				SetProperty(ref contacts, value);
			}
		}

		public AddContactViewModel()
		{
		}
	}
}
