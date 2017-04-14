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
		public void TestConstructor()
		{
			Assert.NotNull(viewModel.Contacts);
			Assert.Equal("Edit Light Bulb", viewModel.Title);
		}

		[Fact]
		public void TestPropertyChanged()
		{
			TestHelper.TestPropertyChanged(viewModel, nameof(viewModel.Title), "test title");
			TestHelper.TestPropertyChanged(viewModel, nameof(viewModel.Name), string.Empty);
			TestHelper.TestPropertyChanged(viewModel, nameof(viewModel.Message), "test message");
			TestHelper.TestPropertyChanged(viewModel, nameof(viewModel.Contacts), new ObservableCollection<ContactViewModel>());
			TestHelper.TestPropertyChanged(viewModel, nameof(viewModel.AddContacts), AwaitableCommand.Empty);
			TestHelper.TestPropertyChanged(viewModel, nameof(viewModel.SaveLightBulb), AwaitableCommand.Empty);
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

		[Fact]
		public async Task TestAddContactSetsTargetCollection()
		{
			var addContactCommand = viewModel.AddContacts;
			navigationService.AssociateViewModelForView<AddContactViewModel, Page>();
			viewModel.Contacts = new ObservableCollection<ContactViewModel>();

			await addContactCommand.ExecuteAsync(null);

			var addContactViewModel = (AddContactViewModel)navigationService.CurrentViewModel;
			Assert.Same(viewModel.Contacts, addContactViewModel.TargetCollection);
		}
	}
}
