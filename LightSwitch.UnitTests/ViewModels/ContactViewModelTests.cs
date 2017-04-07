using System;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class ContactViewModelTests
	{
		[Fact]
		public void TestDefaultConstructor()
		{
			var contactViewModel = new ContactViewModel();
			Assert.NotNull(contactViewModel);
		}

		[Fact]
		public void TestContactProperty()
		{
			var contactViewModel = new ContactViewModel();
			var namePropertyChanged = false;

			contactViewModel.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == nameof(contactViewModel.Name))
				{
					namePropertyChanged = true;
				}
			};

			contactViewModel.Contact = new Contact();

			Assert.True(namePropertyChanged, "PropertyChanged was not raised for \"Name\"");
		}

		[Fact]
		public void TestNameProperty()
		{
			var contactViewModel = new ContactViewModel();
			var result = false;
			contactViewModel.PropertyChanged += (sender, e) => 
			{
				if (e.PropertyName == "Name")
				{
					result = true;
				}
			};

			contactViewModel.Name = "new name";

			Assert.True(result, "PropertyChanged was not raised for \"Name\"");
		}
	}
}
