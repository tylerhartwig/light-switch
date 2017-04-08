using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LightSwitch
{
	public class AddEditLightBulbViewModel : BaseViewModel
	{
		private string title;
		public string Title
		{
			get { return title; }
			set
			{
				SetProperty(ref title, value);
			}
		}

		private ObservableCollection<ContactViewModel> contacts;
		public ObservableCollection<ContactViewModel> Contacts
		{
			get { return contacts; }
			set
			{
				SetProperty(ref contacts, value);
			}
		}

		private IAsyncCommand _addContacts;
		public IAsyncCommand AddContacts
		{
			get { return _addContacts; }
			set
			{
				SetProperty(ref _addContacts, value);
			}
		}

		IAddressBookService addressBookService;
		INavigationService navigationService;

		public AddEditLightBulbViewModel(INavigationService navigationService, IAddressBookService addressBookService)
		{
			Title = "Edit Light Bulb";
			AddContacts = new AwaitableCommand(async () => await addContacts());
			Contacts = new ObservableCollection<ContactViewModel>();

			this.navigationService = navigationService;
			this.addressBookService = addressBookService;
		}

		private async Task addContacts()
		{
			await addressBookService.RequestPermission();
			var contactNames = await addressBookService.GetContactNames();
			var contactViewModels = new ObservableCollection<ContactViewModel>();
			foreach (var contact in contactNames)
			{
				contactViewModels.Add(new ContactViewModel { Name = contact });
			}

			var viewModel = await navigationService.GoToPageForViewModel<AddContactViewModel>();
			viewModel.Contacts = contactViewModels;
			viewModel.TargetCollection = this.Contacts;
		}
	}
}
