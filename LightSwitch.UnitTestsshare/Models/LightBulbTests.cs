using System;
using NUnit.Framework;

namespace LightSwitch.UnitTests
{
	[TestFixture]
	public class LightBulbTests
	{
		[Test]
		public void TestLightBulbDefaultConstructor()
		{
			var lightBulb = new LightBulb();
			Assert.NotNull(lightBulb);
		}

		[Test]
		public void TestEqualObjects()
		{
			var lightBulb1 = new LightBulb { ID = 1 };
			var lightBulb2 = new LightBulb { ID = 1 };

			Assert.AreEqual(lightBulb1, lightBulb2);
		}

		[Test]
		public void TestUnEqualObjects()
		{
			var lightBulb1 = new LightBulb { ID = 0 };
			var lightBulb2 = new LightBulb { ID = 1 };

			Assert.AreNotEqual(lightBulb1, lightBulb2);
		}	}
}
