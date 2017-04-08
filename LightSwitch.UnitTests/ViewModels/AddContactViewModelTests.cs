using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class AddContactViewModelTests
	{
		AddContactViewModel viewModel;

		INavigationService navigationService;

		public AddContactViewModelTests()
		{
			navigationService = new NavigationServiceMock();
			viewModel = new AddContactViewModel(navigationService);
		}

		[Fact]
		public void TestDefaultConstructor()
		{
			Assert.Equal("Add Contact", viewModel.Title);
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
			Assert.True(propertyChanged, "PropertyChanged was not raised for \"Contacts\"");
		}

		[Fact]
		public void TestTitleProperty()
		{
			var propertyChanged = false;

			viewModel.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == nameof(viewModel.Title)) 
				{
					propertyChanged = true;
				}
			};

			viewModel.Title = "new title";
			Assert.True(propertyChanged, string.Format("PropertyChanged was not raised for \"{0}\"", nameof(viewModel.Title)));
		}

		[Fact]
		public void TestTargetCollectionProperty()
		{
			var propertyChanged = false;

			viewModel.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == nameof(viewModel.TargetCollection))
				{
					propertyChanged = true;
				}
			};

			viewModel.TargetCollection = new ObservableCollection<ContactViewModel>();
			Assert.True(propertyChanged, string.Format("PropertyChanged was not raised for \"{0}\"", nameof(viewModel.TargetCollection)));
		}

		[Fact]
		public void TestSelectedContactProperty()
		{
			var propertyChanged = false;

			viewModel.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == nameof(viewModel.SelectedContact))
				{
					propertyChanged = true;
				}
			};

			viewModel.SelectedContact = new ContactViewModel();
			Assert.True(propertyChanged, string.Format("PropertyChanged was not raised for \"{0}\"", nameof(viewModel.SelectedContact)));
		}

		[Fact]
		public void TestAddContactProperty()
		{
			var propertyChanged = false;

			viewModel.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == nameof(viewModel.AddContact))
				{
					propertyChanged = true;
				}
			};

			viewModel.AddContact = new AwaitableCommand(() => Task.Run(() => { }));
			Assert.True(propertyChanged, string.Format("PropertyChanged was not raised for \"{0}\"", nameof(viewModel.AddContact)));
		}

		[Fact]
		public async Task TestAddContactAddsContactToTargetCollection()
		{
			var addContactCommand = viewModel.AddContact;
			viewModel.TargetCollection = new ObservableCollection<ContactViewModel>();
			viewModel.SelectedContact = new ContactViewModel();

			await addContactCommand.ExecuteAsync(null);

			Assert.True(viewModel.TargetCollection.Contains(viewModel.SelectedContact), "SelectedContact was not found in TargetCollection");
		}

		[Fact]
		public async Task TestAddContactRemovesPage()
		{
			viewModel.TargetCollection = new ObservableCollection<ContactViewModel>();
			viewModel.SelectedContact = new ContactViewModel();

			var addContactCommand = viewModel.AddContact;
			await addContactCommand.ExecuteAsync(null);

			Assert.Null(navigationService.CurrentPage);
		}
	}
}
