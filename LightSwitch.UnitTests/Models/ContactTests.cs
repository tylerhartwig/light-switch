using System;
using NUnit.Framework;

namespace LightSwitch.UnitTests
{
	[TestFixture]
	public class ContactTests
	{
		[Test]
		public void TestDefaultConstructor()
		{
			var contact = new Contact();
			Assert.NotNull(contact);
		}

		[Test]
		public void TestGetSetName()
		{
			var name = "test name";
			var contact = new Contact();

			contact.Name = name;
			Assert.AreEqual(name, contact.Name);
		}
	}
}
