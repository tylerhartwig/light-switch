using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class DatabaseServiceMockTests : IDisposable
	{
		protected readonly Contact[] sampleContactData = new Contact[]
		{
			new Contact { Name = "sample contact name 1" },
			new Contact { Name = "sample contact name 2" },
			new Contact { Name = "sample contact name 3" },
			new Contact { Name = "sample contact name 4" }
		};
		protected readonly Message[] sampleMessageData = new Message[]
		{
			new Message { Text = "sample message text 1" },
			new Message { Text = "sample message text 2" },
			new Message { Text = "sample message text 3" },
			new Message { Text = "sample message text 4" },
			new Message { Text = "sample message text 5" },
			new Message { Text = "sample message text 6" }
		};
		protected readonly Quote[] sampleQuoteData = new Quote[]
		{
			new Quote { Text = "inspiring quote 1", Reference = "Reference 1" },
			new Quote { Text = "inspiring quote 2", Reference = "Reference 1" },
			new Quote { Text = "inspiring quote 3", Reference = "Reference 2" },
			new Quote { Text = "inspiring quote 4", Reference = "Reference 3" },
			new Quote { Text = "inspiring quote 5", Reference = "Reference 4" },
			new Quote { Text = "inspiring quote 6", Reference = "Reference 4" },
			new Quote { Text = "inspiring quote 7", Reference = "Reference 1" }
		};
		protected LightBulb[] sampleLightBulbData;

		protected IDatabaseService service;

		public DatabaseServiceMockTests()
		{
			service = new DatabaseServiceMock();
			SetupLightBulbData();
			service.InitializeAsync().Wait();
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

			var beforeCount = await service.GetLightBulbCountAsync();
			await service.AddLightBulbAsync(lightBulb);
			var afterCount = await service.GetLightBulbCountAsync();
			Assert.Equal(beforeCount + 1, afterCount);
		}

		[Fact]
		public async Task TestRemoveLightBulbFromDatabase()
		{
			var lightBulb = new LightBulb();

			await service.AddLightBulbAsync(lightBulb);
			var beforeCount = await service.GetLightBulbCountAsync();
			await service.RemoveLightBulbAsync(lightBulb);
			var afterCount = await service.GetLightBulbCountAsync();
			Assert.Equal(beforeCount - 1, afterCount);
		}

		[Fact]
		public async Task TestGetAllLightBulbFromDatabase()
		{
			var expectedCount = sampleLightBulbData.Length;

			await addAllSampleDataToDatabase();
						
			var actualCount = await service.GetLightBulbCountAsync();
			Assert.Equal(expectedCount, actualCount);

			var lightBulbs = await service.GetAllLightBulbsAsync();
			Assert.NotNull(lightBulbs);

			foreach (var bulb in sampleLightBulbData)
			{
				Assert.True(lightBulbs.Contains(bulb), "Returned LightBulb set does not include all sample data");
			}
		}

		private async Task addAllSampleDataToDatabase()
		{
			foreach (var bulb in sampleLightBulbData)
			{
				await service.AddLightBulbAsync(bulb);
			}
			foreach (var message in sampleMessageData)
			{
				await service.AddMessageAsync(message);
			}
			foreach (var quote in sampleQuoteData)
			{
				await service.AddQuoteAsync(quote);
			}
			foreach (var contact in sampleContactData)
			{
				await service.AddContactAsync(contact);
			}

			foreach (var bulb in sampleLightBulbData)
			{
				foreach (var message in bulb.Messages.ToList())
				{
					await service.AssociateLightBulbWithMessageAsync(bulb, message);
				}
				foreach (var quote in bulb.Quotes.ToList())
				{
					await service.AssociateLightBulbWithQuoteAsync(bulb, quote);
				}
				foreach (var contact in bulb.Contacts.ToList())
				{
					await service.AssociateLightBulbWithContactAsync(bulb, contact);
				}
			}
		}

		[Fact]
		public async Task TestAddContactToDatabase()
		{
			var contact = new Contact();

			var beforeCount = await service.GetContactCountAsync();
			await service.AddContactAsync(contact);
			var afterCount = await service.GetContactCountAsync();

			Assert.Equal(beforeCount + 1, afterCount);
		}

		[Fact]
		public async Task TestRemoveContactFromDatabase()
		{
			var contact = new Contact();

			await service.AddContactAsync(contact);
			var beforeCount = await service.GetContactCountAsync();
			await service.RemoveContactAsync(contact);
			var afterCount = await service.GetContactCountAsync();

			Assert.Equal(beforeCount - 1, afterCount);
		}

		[Fact]
		public async Task TestGetAllContactsFromDatabase()
		{
			var taskList = new List<Task>();
			var expectedCount = sampleContactData.Length;

			foreach (var contact in sampleContactData)
			{
				taskList.Add(service.AddContactAsync(contact));
			}

			await Task.WhenAll(taskList);
			var actualCount = await service.GetContactCountAsync();
			Assert.Equal(expectedCount, actualCount);

			var contacts = await service.GetAllContactsAsync();
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

			var beforeCount = await service.GetMessageCountAsync();
			await service.AddMessageAsync(message);
			var afterCount = await service.GetMessageCountAsync();

			Assert.Equal(beforeCount + 1, afterCount);
		}

		[Fact]
		public async Task TestRemoveMessageFromDatabase()
		{
			var message = new Message();

			await service.AddMessageAsync(message);
			var beforeCount = await service.GetMessageCountAsync();
			await service.RemoveMessageAsync(message);
			var afterCount = await service.GetMessageCountAsync();

			Assert.Equal(beforeCount - 1, afterCount);
		}

		[Fact]
		public async Task TestGetAllMessagesFromDatabase()
		{
			var taskList = new List<Task>();
			var expectedCount = sampleMessageData.Length;

			foreach (var message in sampleMessageData)
			{
				taskList.Add(service.AddMessageAsync(message));
			}

			await Task.WhenAll(taskList);
			var actualCount = await service.GetMessageCountAsync();
			Assert.Equal(expectedCount, actualCount);

			var messages = await service.GetAllMessagesAsync();
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

			var beforeCount = await service.GetQuoteCountAsync();
			await service.AddQuoteAsync(quote);
			var afterCount = await service.GetQuoteCountAsync();

			Assert.Equal(beforeCount + 1, afterCount);
		}

		[Fact]
		public async Task TestRemoveQuoteFromDatabase()
		{
			var quote = new Quote();

			await service.AddQuoteAsync(quote);
			var beforeCount = await service.GetQuoteCountAsync();
			await service.RemoveQuoteAsync(quote);
			var afterCount = await service.GetQuoteCountAsync();

			Assert.Equal(beforeCount - 1, afterCount);
		}

		[Fact]
		public async Task TestGetAllQuotesFromDatabase()
		{
			var taskList = new List<Task>();
			var expectedCount = sampleQuoteData.Length;

			foreach (var quote in sampleQuoteData)
			{
				taskList.Add(service.AddQuoteAsync(quote));
			}

			await Task.WhenAll(taskList);
			var actualCount = await service.GetQuoteCountAsync();
			Assert.Equal(expectedCount, actualCount);

			var quotes = await service.GetAllQuotesAsync();
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
				service.AddLightBulbAsync(lightBulb),
				service.AddMessageAsync(message)
			});

			await service.AssociateLightBulbWithMessageAsync(lightBulb, message);
			var returnedBulb = await service.GetLightBulbAsync(lightBulb.ID);
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
				service.AddLightBulbAsync(lightBulb),
				service.AddQuoteAsync(quote)
			});

			await service.AssociateLightBulbWithQuoteAsync(lightBulb, quote);
			var returnedBulb = await service.GetLightBulbAsync(lightBulb.ID);
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
				service.AddLightBulbAsync(lightBulb),
				service.AddContactAsync(contact)
			});

			await service.AssociateLightBulbWithContactAsync(lightBulb, contact);
			var returnedBulb = await service.GetLightBulbAsync(lightBulb.ID);
			Assert.Equal(lightBulb, returnedBulb);
		}

		public void Dispose()
		{
			service.ResetDatabaseAsync().Wait();
		}
	}
}
