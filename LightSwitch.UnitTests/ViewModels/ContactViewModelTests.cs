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
		public void TestContactConstructor()
		{
			var contact = new Contact
			{
				ID = 777,
				Name = "New name"
			};

			var contactViewModel = new ContactViewModel(contact);

			Assert.Equal(contact.Name, contactViewModel.Name);
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
