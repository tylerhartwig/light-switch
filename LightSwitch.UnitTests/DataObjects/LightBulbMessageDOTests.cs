using System;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class LightBulbMessageDOTests
	{
		[Fact]
		public void TestDefaultConstructor()
		{
			var lightBulbMessageDO = new LightBulbMessageDO();
			Assert.NotNull(lightBulbMessageDO);
		}

		[Fact]
		public void TestProperties()
		{
			var testObject = new LightBulbMessageDO();

			var id = 999;
			testObject.ID = id;
			Assert.Equal(id, testObject.ID);

			var lightBulbDO = 777;
			testObject.LightBulbDO = lightBulbDO;
			Assert.Equal(lightBulbDO, testObject.LightBulbDO);

			var messageID = 888;
			testObject.MessageDO = messageID;
			Assert.Equal(messageID, testObject.MessageDO);
		}
	}
}
