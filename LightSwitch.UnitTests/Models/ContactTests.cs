using System;
using NUnit.Framework;

namespace LightSwitch.UnitTests
{
	[TestFixture]
	public class ContactTests
	{
		private Contact testObject;

		[SetUp]
		public void CreateTestObject()
		{
			testObject = new Contact();
		}

		[Test]
		public void TestDefaultConstructor()
		{
			var contact = new Contact();
			Assert.NotNull(contact);
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
