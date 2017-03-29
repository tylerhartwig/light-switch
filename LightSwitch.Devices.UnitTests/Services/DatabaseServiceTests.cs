using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class DatabaseServiceTests : IDisposable
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
			serviceReference.InitializeAsync().Wait();
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
			};		
		}

		[Fact]
		public async Task TestAddLightBulbToDatabase()
		{
			var lightBulb = new LightBulb();

			var beforeCount = await serviceReference.GetLightBulbCountAsync();
			await serviceReference.AddLightBulbAsync(lightBulb);
			var afterCount = await serviceReference.GetLightBulbCountAsync();
			Assert.Equal(beforeCount + 1, afterCount);
		}

		[Fact]
		public async Task TestRemoveLightBulbFromDatabase()
		{
			var lightBulb = new LightBulb();

			await serviceReference.AddLightBulbAsync(lightBulb);
			var beforeCount = await serviceReference.GetLightBulbCountAsync();
			await serviceReference.RemoveLightBulbAsync(lightBulb);
			var afterCount = await serviceReference.GetLightBulbCountAsync();
			Assert.Equal(beforeCount - 1, afterCount);
		}

		[Fact]
		public async Task TestGetAllLightBulbFromDatabase()
		{
			var expectedCount = sampleLightBulbData.Length;

			await addAllSampleDataToDatabase();
						
			var actualCount = await serviceReference.GetLightBulbCountAsync();
			Assert.Equal(expectedCount, actualCount);

			var lightBulbs = await serviceReference.GetAllLightBulbsAsync();
			Assert.NotNull(lightBulbs);

			foreach (var bulb in sampleLightBulbData)
			{
				Assert.True(lightBulbs.Contains(bulb), "Returned LightBulb set does not include all sample data");
			}
		}

		private async Task addAllSampleDataToDatabase()
		{
			var taskList = new List<Task>();

			foreach (var bulb in sampleLightBulbData)
			{
				taskList.Add(serviceReference.AddLightBulbAsync(bulb));
			}
			foreach (var message in sampleMessageData)
			{
				taskList.Add(serviceReference.AddMessageAsync(message));
			}
			foreach (var quote in sampleQuoteData)
			{
				taskList.Add(serviceReference.AddQuoteAsync(quote));
			}
			foreach (var contact in sampleContactData)
			{
				taskList.Add(serviceReference.AddContactAsync(contact));
			}

			await Task.WhenAll(taskList);

			taskList.Clear();
			foreach (var bulb in sampleLightBulbData)
			{
				foreach (var message in bulb.Messages)
				{
					taskList.Add(serviceReference.AssociateLightBulbWithMessageAsync(bulb, message));
				}
				foreach (var quote in bulb.Quotes)
				{
					taskList.Add(serviceReference.AssociateLightBulbWithQuoteAsync(bulb, quote));
				}
				foreach (var contact in bulb.Contacts)
				{
					taskList.Add(serviceReference.AssociateLightBulbWithContactAsync(bulb, contact));
				}
			}

			await Task.WhenAll(taskList);
		}

		[Fact]
		public async Task TestAddContactToDatabase()
		{
			var contact = new Contact();

			var beforeCount = await serviceReference.GetContactCountAsync();
			await serviceReference.AddContactAsync(contact);
			var afterCount = await serviceReference.GetContactCountAsync();

			Assert.Equal(beforeCount + 1, afterCount);
		}

		[Fact]
		public async Task TestRemoveContactFromDatabase()
		{
			var contact = new Contact();

			await serviceReference.AddContactAsync(contact);
			var beforeCount = await serviceReference.GetContactCountAsync();
			await serviceReference.RemoveContactAsync(contact);
			var afterCount = await serviceReference.GetContactCountAsync();

			Assert.Equal(beforeCount - 1, afterCount);
		}

		[Fact]
		public async Task TestGetAllContactsFromDatabase()
		{
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
				Assert.True(contacts.Contains(contact), "Returned contact set does not contain a piece of sample data");
			}
		}

		[Fact]
		public async Task TestAddMessageToDatabase()
		{
			var message = new Message();

			var beforeCount = await serviceReference.GetMessageCountAsync();
			await serviceReference.AddMessageAsync(message);
			var afterCount = await serviceReference.GetMessageCountAsync();

			Assert.Equal(beforeCount + 1, afterCount);
		}

		[Fact]
		public async Task TestRemoveMessageFromDatabase()
		{
			var message = new Message();

			await serviceReference.AddMessageAsync(message);
			var beforeCount = await serviceReference.GetMessageCountAsync();
			await serviceReference.RemoveMessageAsync(message);
			var afterCount = await serviceReference.GetMessageCountAsync();

			Assert.Equal(beforeCount - 1, afterCount);
		}

		[Fact]
		public async Task TestGetAllMessagesFromDatabase()
		{
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
				Assert.True(messages.Contains(message), "Returned message set does not contain all sample data");
			}
		}

		[Fact]
		public async Task TestAddQuoteToDatabase()
		{
			var quote = new Quote();

			var beforeCount = await serviceReference.GetQuoteCountAsync();
			await serviceReference.AddQuoteAsync(quote);
			var afterCount = await serviceReference.GetQuoteCountAsync();

			Assert.Equal(beforeCount + 1, afterCount);
		}

		[Fact]
		public async Task TestRemoveQuoteFromDatabase()
		{
			var quote = new Quote();

			await serviceReference.AddQuoteAsync(quote);
			var beforeCount = await serviceReference.GetQuoteCountAsync();
			await serviceReference.RemoveQuoteAsync(quote);
			var afterCount = await serviceReference.GetQuoteCountAsync();

			Assert.Equal(beforeCount - 1, afterCount);
		}

		[Fact]
		public async Task TestGetAllQuotesFromDatabase()
		{
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
				Assert.True(quotes.Contains(quote), "Returned quote set does not contain all quotes in sample data");
			}
		}

		[Fact]
		public async Task TestAssociateLightBulbAndMessage()
		{
			var message = new Message
			{
				Text = "test text"
			};

			var lightBulb = new LightBulb
			{
				Name = "test name"
			};
			lightBulb.Messages.Add(message);

			await Task.WhenAll(new Task[] {
				serviceReference.AddLightBulbAsync(lightBulb),
				serviceReference.AddMessageAsync(message)
			});

			await serviceReference.AssociateLightBulbWithMessageAsync(lightBulb, message);
			var returnedBulb = await serviceReference.GetLightBulbAsync(lightBulb.ID);
			Assert.Equal(lightBulb, returnedBulb);
		}

		[Fact]
		public async Task TestAssociateLightBulbAndQuote()
		{
			var quote = new Quote
			{
				Text = "test text",
				Reference = "test reference"
			};

			var lightBulb = new LightBulb
			{
				Name = "test name"
			};
			lightBulb.Quotes.Add(quote);

			await Task.WhenAll(new Task[] {
				serviceReference.AddLightBulbAsync(lightBulb),
				serviceReference.AddQuoteAsync(quote)
			});

			await serviceReference.AssociateLightBulbWithQuoteAsync(lightBulb, quote);
			var returnedBulb = await serviceReference.GetLightBulbAsync(lightBulb.ID);
			Assert.Equal(lightBulb, returnedBulb);
		}

		[Fact]
		public async Task TestAssociateLightBulbAndContact()
		{
			var contact = new Contact
			{
				Name = "test name"
			};

			var lightBulb = new LightBulb
			{
				Name = "test lightbulb name"
			};
			lightBulb.Contacts.Add(contact);

			await Task.WhenAll(new Task[] {
				serviceReference.AddLightBulbAsync(lightBulb),
				serviceReference.AddContactAsync(contact)
			});

			await serviceReference.AssociateLightBulbWithContactAsync(lightBulb, contact);
			var returnedBulb = await serviceReference.GetLightBulbAsync(lightBulb.ID);
			Assert.Equal(lightBulb, returnedBulb);
		}

		public void Dispose()
		{
			serviceReference.ResetDatabaseAsync().Wait();
		}
	}
}
