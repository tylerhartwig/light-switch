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
		IDatabaseService databaseService;
		AddEditLightBulbViewModel viewModel;

		public AddEditLightBulbViewModelTests()
		{
			navigationService = new NavigationServiceMock();
			addressBookService = new AddressBookServiceMock();
			databaseService = new DatabaseServiceMock();
			viewModel = new AddEditLightBulbViewModel(navigationService, addressBookService, databaseService);
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

		[Fact]
		public async Task TestSaveLightBulbCommandSavesBulb()
		{
			var command = viewModel.SaveLightBulb;

			var lightBulb = new LightBulb
			{
				Name = "Stealing Cars",
				Contacts = new List<Contact>(new[]
				{
					new Contact
					{
						Name = "Tyler"
					}
				}),
				Messages = new List<Message>(new[]
				{
					new Message
					{
						Text = "test text"
					}
				})
			};

			viewModel.Name = lightBulb.Name;
			viewModel.Message = lightBulb.Messages.FirstOrDefault().Text;

			foreach (var contact in lightBulb.Contacts)
			{
				viewModel.Contacts.Add(new ContactViewModel
				{
					Name = contact.Name
				});
			}

			await command.ExecuteAsync(null);
			var lightBulbs = await databaseService.GetAllLightBulbsAsync();

			Assert.True(lightBulbs.Contains(lightBulb), "Database does not contain expected lightbulb after save command");
		}

		[Fact]
		public async Task TestSaveLightBulbGoesBack()
		{
			var command = viewModel.SaveLightBulb;
			var currentPage = navigationService.CurrentPage;

			await command.ExecuteAsync(null);
			Assert.NotEqual(currentPage, navigationService.CurrentPage);
		}
	}
}
