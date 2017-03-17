using System;
using NUnit.Framework;

namespace LightSwitch.UnitTests
{
	[TestFixture]
	public class LightBulbTests
	{
		private LightBulb testObject;

		[SetUp]
		public void CreateTestObject()
		{
			testObject = new LightBulb();
		}

		[Test]
		public void TestDefaultConstructor()
		{
			var lightBulb = new LightBulb();
			Assert.NotNull(lightBulb);
		}

		[Test]
		public void TestProperties()
		{
			var id = 777;
			testObject.ID = id;
			Assert.AreEqual(id, testObject.ID, "Set and Got ID do not match");

			var name = "test name";
			testObject.Name = name;
			Assert.AreEqual(name, testObject.Name, "Set and Got Name do not match");
		}
	}
}
