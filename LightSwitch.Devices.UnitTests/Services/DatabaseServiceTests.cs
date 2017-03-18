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
		private readonly LightBulb[] sampleLightBulbData = new LightBulb[]
		{
			new LightBulb { Name = "sample name 1" },
			new LightBulb { Name = "sample name 2" },
			new LightBulb { Name = "sample name 3" }
		};
		private readonly Contact[] sampleContactData = new Contact[]
		{
			new Contact { Name = "sample contact name 1" },
			new Contact { Name = "sample contact name 2" },
			new Contact { Name = "sample contact name 3" },
			new Contact { Name = "sample contact name 4" }
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

			await serviceReference.AddLightBulbAsync(lightBulb);
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
			var expectedCount = sampleLightBulbData.Length;

			foreach (var bulb in sampleLightBulbData)
			{
				taskList.Add(serviceReference.AddLightBulbAsync(bulb));
			}

			await Task.WhenAll(taskList);
			var actualCount = await serviceReference.GetLightBulbCountAsync();
			Assert.Equal(expectedCount, actualCount);

			var lightBulbs = await serviceReference.GetAllLightBulbsAsync();
			Assert.NotNull(lightBulbs);

			foreach (var bulb in sampleLightBulbData)
			{
				Assert.True(lightBulbs.Contains(bulb, new PropertyComparer<LightBulb>()), "Returned LightBulb set does not include all sample data");
			}

			await serviceReference.ResetDatabaseAsync();
		}

		[Fact]
		public async Task TestAddContactToDatabase()
		{
			await serviceReference.InitializeAsync();

			var contact = new Contact();

			var beforeCount = await serviceReference.GetContactCountAsync();
			await serviceReference.AddContactAsync(contact);
			var afterCount = await serviceReference.GetContactCountAsync();

			Assert.Equal(beforeCount + 1, afterCount);

			await serviceReference.ResetDatabaseAsync();
		}

		[Fact]
		public async Task TestRemoveContactFromDatabase()
		{
			await serviceReference.InitializeAsync();

			var contact = new Contact();

			await serviceReference.AddContactAsync(contact);
			var beforeCount = await serviceReference.GetContactCountAsync();
			await serviceReference.RemoveContactAsync(contact);
			var afterCount = await serviceReference.GetContactCountAsync();

			Assert.Equal(beforeCount - 1, afterCount);

			await serviceReference.ResetDatabaseAsync();
		}

		[Fact]
		public async Task TestGetAllContactsFromDatabase()
		{
			await serviceReference.InitializeAsync();

			var taskList = new List<Task>();
			var expectedCount = sampleContactData.Length;

			foreach (var contact in sampleContactData)
			{
				taskList.Add(serviceReference.AddContactAsync(contact));
			}

			await Task.WhenAll(taskList);
			var actualCount = await serviceReference.GetContactCountAsync();
			Assert.Equal(expectedCount, actualCount);

			var contacts = await serviceReference.GetAllContactsAsync();
			Assert.NotNull(contacts);

			foreach (var contact in sampleContactData)
			{
				Assert.True(contacts.Contains(contact, new PropertyComparer<Contact>()), "Returned contact set does not contain a piece of sample data");
			}

			await serviceReference.ResetDatabaseAsync();
		}
	}
}
