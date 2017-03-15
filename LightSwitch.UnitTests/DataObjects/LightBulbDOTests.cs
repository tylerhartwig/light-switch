using System;
using NUnit.Framework;

namespace LightSwitch.UnitTests
{
	[TestFixture]
	public class LightBulbDOTests
	{
		[Test]
		public void TestLightBulbDefaultConstructor()
		{
			var lightBulb = new LightBulbDO();
			Assert.NotNull(lightBulb);
		}

		[Test]
		public void TestEqualObjects()
		{
			var lightBulb1 = new LightBulbDO { ID = 1 };
			var lightBulb2 = new LightBulbDO { ID = 1 };

			Assert.AreEqual(lightBulb1, lightBulb2);
		}

		[Test]
		public void TestUnEqualObjects()
		{
			var lightBulb1 = new LightBulbDO { ID = 0 };
			var lightBulb2 = new LightBulbDO { ID = 1 };

			Assert.AreNotEqual(lightBulb1, lightBulb2);
		}	}
}
