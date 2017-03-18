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
	}
}
