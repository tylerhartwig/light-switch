using System;
using NUnit.Framework;

namespace LightSwitch.UnitTests
{
	[TestFixture]
	public class LightBulbDOTests
	{
		private LightBulbDO testObject;

		[SetUp]
		public void CreateLightBulbDO()
		{
			testObject = new LightBulbDO();
		}

		[Test]
		public void TestDefaultConstructor()
		{
			var lightBulbDO = new LightBulbDO();
			Assert.NotNull(lightBulbDO, "Default Constructor failed to create object");
		}

		[Test]
		public void TestLightBulbConstructor()
		{
			var name = "test name";
			var lightBulb = new LightBulb
			{
				ID = 777,
				Name = name
			};

			var lightBulbDO = new LightBulbDO(lightBulb);
			Assert.NotNull(lightBulbDO, "LightBulbConstructor failed to create an object");

			Assert.AreEqual(lightBulb.ID, lightBulbDO.ID, "LightBulb Constructor failed to duplicate ID");
			Assert.AreEqual(lightBulb.Name, lightBulbDO.Name, "LightBulb Constructor failed to duplicate Name");

		}

		[Test]
		public void TestProperties()
		{
			var id = 777;
			testObject.ID = id;
			Assert.AreEqual(id, testObject.ID, "Failed to set or get ID they are unequal");

			var name = "test name";
			testObject.Name = name;
			Assert.AreEqual(name, testObject.Name, "Failed to set or get Name, they are unequal");
		}

		[Test]
		public void TestEqualObjects()
		{

			var lightBulbDO1 = new LightBulbDO();
			var lightBulbDO2 = new LightBulbDO();

			Assert.AreEqual(lightBulbDO1, lightBulbDO2, "Equal DO objects are not equal");

			lightBulbDO1.ID = 1;
			lightBulbDO2.ID = 1;

			Assert.AreEqual(lightBulbDO1, lightBulbDO2, "Equal DO objects are not equal after ID change");

			var name = "test name";
			lightBulbDO1.Name = name;
			lightBulbDO2.Name = name;

			Assert.AreEqual(lightBulbDO1, lightBulbDO2, "Equal DO objects are not equal after name change");
		}

		[Test]
		public void TestUnEqualObjects()
		{
			var lightBulbDO1 = new LightBulbDO { ID = 0 };
			var lightBulbDO2 = new LightBulbDO { ID = 1 };
			Assert.AreNotEqual(lightBulbDO1, lightBulbDO2, "UnEqual DO objects are equal with an ID change");

			lightBulbDO1.ID = lightBulbDO2.ID;
			lightBulbDO1.Name = "test name 1";
			lightBulbDO2.Name = "test name 2";
			Assert.AreNotEqual(lightBulbDO1, lightBulbDO2, "UnEqual DO objects are equal with an Name change");
		}

	}
}
