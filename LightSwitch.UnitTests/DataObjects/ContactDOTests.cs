using System;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class ContactDOTests
	{
		[Fact]
		public void TestDefaultConstructor()
		{
			var contactDO = new ContactDO();
			Assert.NotNull(contactDO);
		}

		[Fact]
		public void TestContactConstructor() 
		{
			var contact = new Contact
			{
				ID = 777,
				Name = "contact name"
			};

			var contactDO = new ContactDO(contact);
			Assert.NotNull(contactDO);

			Assert.Equal(contact.ID, contactDO.ID);
			Assert.Equal(contact.Name, contactDO.Name);
		}

		[Fact]
		public void TestProperties()
		{
			var testObject = new ContactDO();

			var id = 777;
			testObject.ID = id;
			Assert.Equal(id, testObject.ID);

			var name = "test name";
			testObject.Name = name;
			Assert.Equal(name, testObject.Name);
		}	
	}
}
