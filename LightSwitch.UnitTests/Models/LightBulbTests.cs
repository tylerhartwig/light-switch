using System;
using NUnit.Framework;

namespace LightSwitch.UnitTests
{
	[TestFixture]
	public class LightBulbTests
	{
		[Test]
		public void TestDefaultConstructor()
		{
			var lightBulb = new LightBulb();
			Assert.NotNull(lightBulb);
		}

		[Test]
		public void TestGetSetName()
		{
			var name = "test name";
			var lightBulb = new LightBulb();

			lightBulb.Name = name;
			Assert.AreEqual(name, lightBulb.Name);
		}
	}
}
