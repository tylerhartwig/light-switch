using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LightSwitch.UnitTests
{
	[TestFixture]
	public class LightBulbDatabaseServiceTests
	{
		private LightBulbDatabaseService serviceReference;

		[SetUp]
		public void DatabaseSingletonTest()
		{
			serviceReference = LightBulbDatabaseService.Instance;
		}

		[Test]
		public void AddLightBulbToDatabase()
		{
			var lightBulb = new LightBulb();

			int beforeCount = 0;
			serviceReference.GetLightBulbCountAsync().ContinueWith(t => beforeCount = t.Result).Wait();
			serviceReference.AddLightBulbAsync(lightBulb).Wait();
			int afterCount = 0;
			serviceReference.GetLightBulbCountAsync().ContinueWith(t => afterCount = t.Result).Wait();
			Assert.AreEqual(beforeCount + 1, afterCount);
		}
	}
}
