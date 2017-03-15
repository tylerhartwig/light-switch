using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LightSwitch.UnitTests
{
	[TestFixture]
	public class DatabaseServiceTests
	{
		private DatabaseService serviceReference;
		private readonly LightBulb[] sampleData = new LightBulb[]{
			new LightBulb{},
			new LightBulb{},
			new LightBulb{}
		};

		[SetUp]
		public void TestSetup()
		{
			GetDatabaseSingleton();
			serviceReference.InitializeAsync().Wait();
		}

		public void GetDatabaseSingleton()
		{
			serviceReference = DatabaseService.Instance;
			Assert.NotNull(serviceReference);
		}

		[TearDown]
		public void ResetDatabase()
		{
			serviceReference.ResetDatabaseAsync().Wait();
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
			var taskList = new List<Task>();
			var expectedCount = sampleData.Length;
			int actualCount = 0;

			foreach (var bulb in sampleData)
			{
				taskList.Add(serviceReference.AddLightBulbAsync(bulb));
			}

			Task.WhenAll(taskList).Wait();
			serviceReference.GetLightBulbCountAsync().ContinueWith(t => actualCount = t.Result).Wait();
			Assert.AreEqual(expectedCount, actualCount, "Did not add all sample data to database");

			List<LightBulb> lightBulbs = null;
			serviceReference.GetAllLightBulbsAsync().ContinueWith(t => lightBulbs = t.Result).Wait();
			Assert.NotNull(lightBulbs, "Failed to retrieve all light bulbs from database");

			foreach (var bulb in sampleData)
			{
				Assert.True(lightBulbs.Contains(bulb), "Returned set does not include all sample data");
			}
		}
	}
}
