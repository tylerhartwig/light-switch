using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

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

		private ContactViewModel selectedContact;
		public ContactViewModel SelectedContact
		{
			get { return selectedContact; }
			set
			{
				SetProperty(ref selectedContact, value);
			}
		}

		private ObservableCollection<ContactViewModel> targetCollection;
		public ObservableCollection<ContactViewModel> TargetCollection
		{
			get { return targetCollection; }
			set
			{
				SetProperty(ref targetCollection, value);
			}
		}

		private IAsyncCommand _addContact;
		public IAsyncCommand AddContact
		{
			get { return _addContact; }
			set
			{
				SetProperty(ref _addContact, value);
			}
		}

		private string title;
		public string Title
		{
			get { return title; }
			set
			{
				SetProperty(ref title, value);
			}
		}

		INavigationService navigationService;

		public AddContactViewModel(INavigationService navigationService)
		{
			Title = "Add Contact";
			AddContact = new AwaitableCommand(addContact);

			this.navigationService = navigationService;
		}

		private async Task addContact()
		{
			TargetCollection.Add(selectedContact);
			await navigationService.GoBack();
		}
	}
}
