using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class DatabaseServiceTests 
	{
		private DatabaseService serviceReference;
		private readonly LightBulb[] sampleData = new LightBulb[]{
			new LightBulb{ Name = "sample name 1" },
			new LightBulb{ Name = "sample name 2" },
			new LightBulb{ Name = "sample name 3" }
		};

		public DatabaseServiceTests()
		{
			GetDatabaseSingleton();
		}

		public void GetDatabaseSingleton()
		{
			serviceReference = DatabaseService.Instance;
			Assert.NotNull(serviceReference);
		}

		[Fact]
		public async Task TestAddLightBulbToDatabase()
		{
			await serviceReference.InitializeAsync();

			var lightBulb = new LightBulb();

			var beforeCount = await serviceReference.GetLightBulbCountAsync();
			await serviceReference.AddLightBulbAsync(lightBulb);
			var afterCount = await serviceReference.GetLightBulbCountAsync();
			Assert.Equal(beforeCount + 1, afterCount);

			await serviceReference.ResetDatabaseAsync();
		}

		[Fact]
		public async Task TestRemoveLightBulbFromDatabase()
		{
			await serviceReference.InitializeAsync();

			var lightBulb = new LightBulb();

			serviceReference.AddLightBulbAsync(lightBulb).Wait();
			var beforeCount = await serviceReference.GetLightBulbCountAsync();
			await serviceReference.RemoveLightBulbAsync(lightBulb);
			var afterCount = await serviceReference.GetLightBulbCountAsync();
			Assert.Equal(beforeCount - 1, afterCount);

			await serviceReference.ResetDatabaseAsync();
		}

		[Fact]
		public async Task TestGetAllLightBulbFromDatabase()
		{
			await serviceReference.InitializeAsync();

			var taskList = new List<Task>();
			var expectedCount = sampleData.Length;

			foreach (var bulb in sampleData)
			{
				taskList.Add(serviceReference.AddLightBulbAsync(bulb));
			}

			await Task.WhenAll(taskList);
			var actualCount = await serviceReference.GetLightBulbCountAsync();
			Assert.Equal(expectedCount, actualCount);

			var lightBulbs = await serviceReference.GetAllLightBulbsAsync();
			Assert.NotNull(lightBulbs);

			foreach (var bulb in sampleData)
			{
				Assert.True(lightBulbs.Contains(bulb), "Returned set does not include all sample data");
			}

			await serviceReference.ResetDatabaseAsync();
		}
	}
}
