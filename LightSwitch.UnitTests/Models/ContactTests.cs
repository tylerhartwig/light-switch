using System;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class ContactTests
	{
		[Fact]
		public void TestDefaultConstructor()
		{
			var contact = new Contact();
			Assert.NotNull(contact);
		}

		[Fact]
		public void TestContactDOConstructor()
		{
			var contactDO = new ContactDO
			{
				ID = 777,
				Name = "Test name"
			};

			var contact = new Contact(contactDO);
			Assert.Equal(contactDO.ID, contact.ID);
			Assert.Equal(contactDO.Name, contact.Name);
		}

		[Fact]
		public void TestProperties()
		{
			var testObject = new Contact();

			var id = 777;
			testObject.ID = id;
			Assert.Equal(id, testObject.ID);

			var name = "test name";
			testObject.Name = name;
			Assert.Equal(name, testObject.Name);
		}

		[Fact]
		public void TestEqualObjectsAreEqual()
		{
			var id = 777;
			var name = "Bobby";

			var leftContact = new Contact
			{
				ID = id,
				Name = name
			};

			var rightContact = new Contact
			{
				ID = id,
				Name = name
			};

			Assert.Equal(leftContact, rightContact);
		}

		[Fact]
		public void TestUnEqualObjectsAreUnEqual()
		{
			var id1 = 777;
			var id2 = 888;
			var name1 = "Bobby";
			var name2 = "Billy";

			var leftContact = new Contact
			{
				ID = id1,
				Name = name1
			};

			var rightContact = new Contact
			{
				ID = id1,
				Name = name2
			};

			Assert.NotEqual(leftContact, rightContact);

			rightContact.ID = id2;
			rightContact.Name = name1;

			Assert.NotEqual(leftContact, rightContact);
		}
	}
}
