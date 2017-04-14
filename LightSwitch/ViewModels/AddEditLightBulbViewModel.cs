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

		private string name;
		public string Name
		{
			get { return name; }
			set
			{
				SetProperty(ref name, value);
			}
		}

		private string message;
		public string Message
		{
			get { return message; }
			set
			{
				SetProperty(ref message, value);
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

		private IAsyncCommand _saveLightBulb;
		public IAsyncCommand SaveLightBulb
		{
			get { return _saveLightBulb; }
			set
			{
				SetProperty(ref _saveLightBulb, value);
			}
		}

		IAddressBookService addressBookService;
		INavigationService navigationService;
		IDatabaseService databaseService;

		public AddEditLightBulbViewModel(INavigationService navigationService, IAddressBookService addressBookService, IDatabaseService databaseService)
		{
			Title = "Edit Light Bulb";
			AddContacts = new AwaitableCommand(addContacts);
			SaveLightBulb = new AwaitableCommand(saveLightBulb);
			Contacts = new ObservableCollection<ContactViewModel>();

			this.navigationService = navigationService;
			this.addressBookService = addressBookService;
			this.databaseService = databaseService;
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

		private async Task saveLightBulb()
		{
			var lightBulb = new LightBulb { Name = this.Name };
			var newMessage = new Message { Text = Message };

			await databaseService.AddLightBulbAsync(lightBulb);

			await databaseService.AddMessageAsync(newMessage);
			await databaseService.AssociateLightBulbWithMessageAsync(lightBulb, newMessage);

			foreach (var contactViewModel in Contacts)
			{
				await databaseService.AddContactAsync(contactViewModel.Contact);
				await databaseService.AssociateLightBulbWithContactAsync(lightBulb, contactViewModel.Contact);
			}

			await navigationService.GoBack();
		}
	}
}
