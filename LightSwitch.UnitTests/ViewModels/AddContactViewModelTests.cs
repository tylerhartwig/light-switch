using System;
using System.Collections.ObjectModel;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class AddContactViewModelTests
	{
		[Fact]
		public void TestDefaultConstructor()
		{
			var viewModel = new AddContactViewModel();
			Assert.NotNull(viewModel);
		}

		[Fact]
		public void TestContactsProperty()
		{
			var viewModel = new AddContactViewModel();

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
	}
}
