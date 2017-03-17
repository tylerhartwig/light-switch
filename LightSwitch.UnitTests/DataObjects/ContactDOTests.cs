using System;
using NUnit.Framework;

namespace LightSwitch.UnitTests
{
	[TestFixture]
	public class ContactDOTests
	{
		private ContactDO testObject;

		[SetUp]
		public void CreateTestObject()
		{
			testObject = new ContactDO();
		}

		[Test]
		public void TestDefaultConstructor()
		{
			var contactDO = new ContactDO();
			Assert.NotNull(contactDO, "Default constructor returned null");
		}

		[Test]
		public void TestContactConstructor() 
		{
			var contact = new Contact
			{
				ID = 777,
				Name = "contact name"
			};

			var contactDO = new ContactDO(contact);
			Assert.NotNull(contactDO, "Contact Constructor failed to create an object");

			Assert.AreEqual(contact.ID, contactDO.ID, "Contact Constructor failed to duplicate ID");
			Assert.AreEqual(contact.Name, contactDO.Name, "Contact Constructor failed to duplicate Name");
		}

		[Test]
		public void TestProperties()
		{
			var id = 777;
			testObject.ID = id;
			Assert.AreEqual(id, testObject.ID, "Set ID does not match Get ID");

			var name = "test name";
			testObject.Name = name;
			Assert.AreEqual(name, testObject.Name, "Set Name does not match Get Name");
		}	
	}
}
