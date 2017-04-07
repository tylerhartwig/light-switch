using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class AddEditLightBulbViewModelTests
	{
		INavigationService navigationService;
		IAddressBookService addressBookService;
		AddEditLightBulbViewModel viewModel;

		public AddEditLightBulbViewModelTests()
		{
			navigationService = new NavigationServiceMock();
			addressBookService = new AddressBookServiceMock();
			viewModel = new AddEditLightBulbViewModel(navigationService, addressBookService);
		}

		[Fact]
		public void TestTitleProperty()
		{

			var title = "new title";
			var propertyChanged = false;

			viewModel.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == "Title")
				{
					propertyChanged = true;
				}
			};

			viewModel.Title = title;
			Assert.True(propertyChanged, "PropertyChanged was not raised for property \"Title\"");
		}

		[Fact]
		public void TestContactsProperty()
		{
			var contacts = new ObservableCollection<ContactViewModel>();
			var propertyChanged = false;

			viewModel.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == "Contacts")
				{
					propertyChanged = true;
				}
			};

			viewModel.Contacts = contacts;
			Assert.True(propertyChanged, "PropertyChanged was not raised for property \"Contacts\"");
		}

		[Fact]
		public void TestAddContactsProperty()
		{
			var addContactsCommand = new AwaitableCommand(async () => { });
			var propertyChanged = false;

			viewModel.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == "AddContacts")
				{
					propertyChanged = true;
				}
			};

			viewModel.AddContacts = addContactsCommand;
			Assert.True(propertyChanged, "PropertyChanged was not raised for property \"AddContacts\"");
		}

		[Fact]
		public async Task TestAddContactsGoesToCorrectScreen()
		{
			var addContactCommand = viewModel.AddContacts;
			navigationService.AssociateViewModelForView<AddContactViewModel, Page>();
			await addContactCommand.ExecuteAsync(null);

			await addressBookService.RequestPermission();
			var contacts = await addressBookService.GetContactNames();
			var contactViewModels = new List<ContactViewModel>();
			foreach (var contact in contacts)
			{
				contactViewModels.Add(new ContactViewModel { Name = contact });
			}

			Assert.True(navigationService.CurrentViewModel is AddContactViewModel, "AddContacts Command did not move to the correct view model");
			var vm = (AddContactViewModel)navigationService.CurrentViewModel;
			Assert.Equal(contactViewModels, vm.Contacts.ToList());
		}
	}
}
