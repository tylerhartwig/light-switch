using System;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class LightBulbDOTests
	{
		[Fact]
		public void TestDefaultConstructor()
		{
			var lightBulbDO = new LightBulbDO();
			Assert.NotNull(lightBulbDO);
		}

		[Fact]
		public void TestLightBulbConstructor()
		{
			var name = "test name";
			var lightBulb = new LightBulb
			{
				ID = 777,
				Name = name
			};

			var lightBulbDO = new LightBulbDO(lightBulb);
			Assert.NotNull(lightBulbDO);

			Assert.Equal(lightBulb.ID, lightBulbDO.ID);
			Assert.Equal(lightBulb.Name, lightBulbDO.Name);

		}

		[Fact]
		public void TestProperties()
		{
			var testObject = new LightBulbDO();
			var id = 777;
			testObject.ID = id;
			Assert.Equal(id, testObject.ID);

			var name = "test name";
			testObject.Name = name;
			Assert.Equal(name, testObject.Name);
		}

		[Fact]
		public void TestEqualObjects()
		{

			var lightBulbDO1 = new LightBulbDO();
			var lightBulbDO2 = new LightBulbDO();

			Assert.Equal(lightBulbDO1, lightBulbDO2);

			lightBulbDO1.ID = 1;
			lightBulbDO2.ID = 1;

			Assert.Equal(lightBulbDO1, lightBulbDO2);

			var name = "test name";
			lightBulbDO1.Name = name;
			lightBulbDO2.Name = name;

			Assert.Equal(lightBulbDO1, lightBulbDO2);
		}

		[Fact]
		public void TestUnEqualObjects()
		{
			var lightBulbDO1 = new LightBulbDO { ID = 0 };
			var lightBulbDO2 = new LightBulbDO { ID = 1 };
			Assert.NotEqual(lightBulbDO1, lightBulbDO2);

			lightBulbDO1.ID = lightBulbDO2.ID;
			lightBulbDO1.Name = "test name 1";
			lightBulbDO2.Name = "test name 2";
			Assert.NotEqual(lightBulbDO1, lightBulbDO2);
		}

	}
}
