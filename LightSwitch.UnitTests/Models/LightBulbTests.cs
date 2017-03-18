using System;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class LightBulbTests
	{
		[Fact]
		public void TestDefaultConstructor()
		{
			var lightBulb = new LightBulb();
			Assert.NotNull(lightBulb);
		}

		[Fact]
		public void TestLightBulbDOConstructor()
		{
			var lightBulbDO = new LightBulbDO
			{
				ID = 777,
				Name = "Test name"
			};

			var lightBulb = new LightBulb(lightBulbDO);
			Assert.NotNull(lightBulb);
			Assert.Equal(lightBulbDO.ID, lightBulb.ID);
			Assert.Equal(lightBulbDO.Name, lightBulb.Name);
		}

		[Fact]
		public void TestProperties()
		{
			var testObject = new LightBulb();

			var id = 777;
			testObject.ID = id;
			Assert.Equal(id, testObject.ID);

			var name = "test name";
			testObject.Name = name;
			Assert.Equal(name, testObject.Name);
		}
	}
}
