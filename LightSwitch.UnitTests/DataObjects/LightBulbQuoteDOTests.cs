using System;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class LightBulbQuoteDOTests
	{
		[Fact]
		public void TestDefaultConstructor()
		{
			var lightBulbQuoteDO = new LightBulbQuoteDO();
			Assert.NotNull(lightBulbQuoteDO);
		}

		[Fact]
		public void TestProperties()
		{
			var testObject = new LightBulbQuoteDO();

			var id = 777;
			testObject.ID = id;
			Assert.Equal(id, testObject.ID);

			var lightBulbID = 888;
			testObject.LightBulbDO = lightBulbID;
			Assert.Equal(lightBulbID, testObject.LightBulbDO);

			var quoteDO = 999;
			testObject.QuoteDO = quoteDO;
			Assert.Equal(quoteDO, testObject.QuoteDO);
		}
	}
}
