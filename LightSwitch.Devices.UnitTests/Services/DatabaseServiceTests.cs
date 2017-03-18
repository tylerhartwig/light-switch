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

		private readonly Contact[] sampleContactData = new Contact[]
		{
			new Contact { Name = "sample contact name 1" },
			new Contact { Name = "sample contact name 2" },
			new Contact { Name = "sample contact name 3" },
			new Contact { Name = "sample contact name 4" }
		};
		private readonly Message[] sampleMessageData = new Message[]
		{
			new Message { Text = "sample message text 1" },
			new Message { Text = "sample message text 2" },
			new Message { Text = "sample message text 3" },
			new Message { Text = "sample message text 4" },
			new Message { Text = "sample message text 5" },
			new Message { Text = "sample message text 6" }
		};
		private readonly Quote[] sampleQuoteData = new Quote[]
		{
			new Quote { Text = "inspiring quote 1", Reference = "Reference 1" },
			new Quote { Text = "inspiring quote 2", Reference = "Reference 1" },
			new Quote { Text = "inspiring quote 3", Reference = "Reference 2" },
			new Quote { Text = "inspiring quote 4", Reference = "Reference 3" },
			new Quote { Text = "inspiring quote 5", Reference = "Reference 4" },
			new Quote { Text = "inspiring quote 6", Reference = "Reference 4" },
			new Quote { Text = "inspiring quote 7", Reference = "Reference 1" }
		};
		private LightBulb[] sampleLightBulbData;

		public DatabaseServiceTests()
		{
			GetDatabaseSingleton();
			SetupLightBulbData();
		}

		public void GetDatabaseSingleton()
		{
			serviceReference = DatabaseService.Instance;
			Assert.NotNull(serviceReference);
		}

		public void SetupLightBulbData()
		{
			sampleLightBulbData = new LightBulb[]
			{
				new LightBulb 
				{ 
					Name = "sample name 1", 
					Messages = new List<Message>(sampleMessageData), 
					Contacts = new List<Contact>(sampleContactData), 
					Quotes = new List<Quote>(sampleQuoteData) 
				},
				new LightBulb 
				{ 
					Name = "sample name 2",
					Messages = new List<Message>(sampleMessageData), 
					Contacts = new List<Contact>(sampleContactData), 
					Quotes = new List<Quote>(sampleQuoteData) 
				},
				new LightBulb
				{
					Name = "sample name 3",
					Messages = new List<Message>(sampleMessageData),
					Contacts = new List<Contact>(sampleContactData),
					Quotes = new List<Quote>(sampleQuoteData)
				}
			};		}

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

		[Fact]
		public async Task TestAddMessageToDatabase()
		{
			await serviceReference.InitializeAsync();

			var message = new Message();

			var beforeCount = await serviceReference.GetMessageCountAsync();
			await serviceReference.AddMessageAsync(message);
			var afterCount = await serviceReference.GetMessageCountAsync();

			Assert.Equal(beforeCount + 1, afterCount);

			await serviceReference.ResetDatabaseAsync();
		}

		[Fact]
		public async Task TestRemoveMessageFromDatabase()
		{
			await serviceReference.InitializeAsync();

			var message = new Message();

			await serviceReference.AddMessageAsync(message);
			var beforeCount = await serviceReference.GetMessageCountAsync();
			await serviceReference.RemoveMessageAsync(message);
			var afterCount = await serviceReference.GetMessageCountAsync();

			Assert.Equal(beforeCount - 1, afterCount);

			await serviceReference.ResetDatabaseAsync();
		}

		[Fact]
		public async Task TestGetAllMessagesFromDatabase()
		{
			await serviceReference.InitializeAsync();

			var taskList = new List<Task>();
			var expectedCount = sampleMessageData.Length;

			foreach (var message in sampleMessageData)
			{
				taskList.Add(serviceReference.AddMessageAsync(message));
			}

			await Task.WhenAll(taskList);
			var actualCount = await serviceReference.GetMessageCountAsync();
			Assert.Equal(expectedCount, actualCount);

			var messages = await serviceReference.GetAllMessagesAsync();
			Assert.NotNull(messages);

			foreach (var message in sampleMessageData)
			{
				Assert.True(messages.Contains(message, new PropertyComparer<Message>()), "Returned message set does not contain all sample data");
			}

			await serviceReference.ResetDatabaseAsync();
		}

		[Fact]
		public async Task TestAddQuoteToDatabase()
		{
			await serviceReference.InitializeAsync();

			var quote = new Quote();

			var beforeCount = await serviceReference.GetQuoteCountAsync();
			await serviceReference.AddQuoteAsync(quote);
			var afterCount = await serviceReference.GetQuoteCountAsync();

			Assert.Equal(beforeCount + 1, afterCount);

			await serviceReference.ResetDatabaseAsync();
		}

		[Fact]
		public async Task TestRemoveQuoteFromDatabase()
		{
			await serviceReference.InitializeAsync();

			var quote = new Quote();

			await serviceReference.AddQuoteAsync(quote);
			var beforeCount = await serviceReference.GetQuoteCountAsync();
			await serviceReference.RemoveQuoteAsync(quote);
			var afterCount = await serviceReference.GetQuoteCountAsync();

			Assert.Equal(beforeCount - 1, afterCount);

			await serviceReference.ResetDatabaseAsync();
		}

		[Fact]
		public async Task TestGetAllQuotesFromDatabase()
		{
			await serviceReference.InitializeAsync();

			var taskList = new List<Task>();
			var expectedCount = sampleQuoteData.Length;

			foreach (var quote in sampleQuoteData)
			{
				taskList.Add(serviceReference.AddQuoteAsync(quote));
			}

			await Task.WhenAll(taskList);
			var actualCount = await serviceReference.GetQuoteCountAsync();
			Assert.Equal(expectedCount, actualCount);

			var quotes = await serviceReference.GetAllQuotesAsync();
			Assert.NotNull(quotes);

			foreach (var quote in sampleQuoteData)
			{
				Assert.True(quotes.Contains(quote, new PropertyComparer<Quote>()), "Returned quote set does not contain all quotes in sample data");
			}

			await serviceReference.ResetDatabaseAsync();
		}
	}
}
