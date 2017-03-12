using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LightSwitch.UnitTests
{
	[TestFixture]
	public class LightBulbDatabaseServiceTests
	{
		private LightBulbDatabaseService serviceReference;
		private readonly LightBulb[] sampleData = new LightBulb[]{
			new LightBulb{},
			new LightBulb{},
			new LightBulb{}
		};

		[SetUp]
		public void TestSetup()
		{
			GetDatabaseSingleton();
		}

		public void GetDatabaseSingleton()
		{
			serviceReference = LightBulbDatabaseService.Instance;
			Assert.NotNull(serviceReference);
		}

		[TearDown]
		public void RemoveDatabase()
		{

		}

		[Test]
		public void TestAddLightBulbToDatabase()
		{
			var lightBulb = new LightBulb();
			int beforeCount = 0, afterCount = 0;

			serviceReference.GetLightBulbCountAsync().ContinueWith(t => beforeCount = t.Result).Wait();
			serviceReference.AddLightBulbAsync(lightBulb).Wait();
			serviceReference.GetLightBulbCountAsync().ContinueWith(t => afterCount = t.Result).Wait();
			Assert.AreEqual(beforeCount + 1, afterCount);
		}

		[Test]
		public void TestRemoveLightBulbFromDatabase()
		{
			var lightBulb = new LightBulb();
			int beforeCount = 0, afterCount = 0;

			serviceReference.AddLightBulbAsync(lightBulb).Wait();
			serviceReference.GetLightBulbCountAsync().ContinueWith(t => beforeCount = t.Result).Wait();
			serviceReference.RemoveLightBulbAsync(lightBulb).Wait();
			serviceReference.GetLightBulbCountAsync().ContinueWith(t => afterCount = t.Result).Wait();
			Assert.AreEqual(beforeCount - 1, afterCount);
		}

		[Test]
		public void TestGetAllLightBulbFromDatabase()
		{

		}
	}
}
