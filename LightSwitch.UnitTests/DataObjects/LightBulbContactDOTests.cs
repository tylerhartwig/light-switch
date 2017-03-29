using System;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class LightBulbContactDOTests
	{
		[Fact]
		public void TestDefaultConstructor()
		{
			var lightBulbContactDO = new LightBulbContactDO();
			Assert.NotNull(lightBulbContactDO);
		}

		[Fact]
		public void TestProperties()
		{
			var testObject = new LightBulbContactDO();

			var id = 777;
			testObject.ID = id;
			Assert.Equal(id, testObject.ID);

			var lightBulbID = 888;
			testObject.LightBulbDO = lightBulbID;
			Assert.Equal(lightBulbID, testObject.LightBulbDO);

			var contactID = 999;
			testObject.ContactDO = contactID;
			Assert.Equal(contactID, testObject.ContactDO);
		}
	}
}
