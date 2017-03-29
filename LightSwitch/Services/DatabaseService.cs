using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace LightSwitch
{
	public class DatabaseService
	{
		private readonly string databaseName = "LightSwitchSQLite.db3";

		private SQLiteAsyncConnection _database;
		private SQLiteAsyncConnection database
		{
			get
			{
				if (_database == null)
				{
					var dbPath = DependencyService.Get<IDatabaseHelper>().GetDatabasePath(databaseName);
					_database = new SQLiteAsyncConnection(dbPath);
				}

				return _database;
			}
		}

		private static DatabaseService instance;
		public static DatabaseService Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new DatabaseService();
				}
				return instance;
			}
		}

		private DatabaseService() { }

		public Task InitializeAsync()
		{
			return database.CreateTablesAsync(CreateFlags.None, new Type[] {
				typeof(LightBulbDO),
				typeof(ContactDO),
				typeof(MessageDO),
				typeof(QuoteDO),
				typeof(LightBulbMessageDO),
				typeof(LightBulbQuoteDO),
				typeof(LightBulbContactDO)
			});
		}

		public Task ResetDatabaseAsync()
		{
			return Task.WhenAll(new Task[] {
				database.DropTableAsync<LightBulbDO>(),
				database.DropTableAsync<ContactDO>(),
				database.DropTableAsync<MessageDO>(),
				database.DropTableAsync<QuoteDO>(),
				database.DropTableAsync<LightBulbMessageDO>(),
				database.DropTableAsync<LightBulbQuoteDO>(),
				database.DropTableAsync<LightBulbContactDO>()
			});
		}

		public Task<int> GetLightBulbCountAsync()
		{
			return database.Table<LightBulbDO>().CountAsync();
		}

		public async Task AddLightBulbAsync(LightBulb lightBulb)
		{
			var lightBulbDO = new LightBulbDO(lightBulb);

			await database.InsertAsync(lightBulbDO);
			lightBulb.ID = lightBulbDO.ID;
		}

		private Task AddAllMessagesAsync(IEnumerable<Message> messages)
		{
			var taskList = new List<Task>();

			foreach (var message in messages)
			{
				taskList.Add(new Task(async () =>
				{
					var messageDO = new MessageDO(message);
					await database.InsertAsync(messageDO);
					message.ID = messageDO.ID;
				}));
			}

			return Task.WhenAll(taskList);
		}

		private Task AddAllContactsAsync(IEnumerable<Contact> contacts)
		{
			var taskList = new List<Task>();

			foreach (var contact in contacts)
			{
				taskList.Add(new Task(async () =>
				{
					var contactDO = new ContactDO(contact);
					await database.InsertAsync(contactDO);
					contact.ID = contactDO.ID;
				}));
			}

			return Task.WhenAll(taskList);
		}

		private Task AddAllQuotesAsync(IEnumerable<Quote> quotes)
		{
			var taskList = new List<Task>();

			foreach (var quote in quotes)
			{
				taskList.Add(new Task(async () =>
				{
					var quoteDO = new QuoteDO(quote);
					await database.InsertAsync(quoteDO);
					quote.ID = quoteDO.ID;
				}));
			}

			return Task.WhenAll(taskList);
		}

		public Task RemoveLightBulbAsync(LightBulb lightBulb)
		{
			var lightBulbDO = new LightBulbDO(lightBulb);
			return database.DeleteAsync(lightBulbDO);
		}

		public async Task<LightBulb> GetLightBulbAsync(int ID)
		{
			var lightBulbDO = await database.Table<LightBulbDO>().Where(lb => lb.ID == ID).FirstOrDefaultAsync();
			var lightBulb = new LightBulb(lightBulbDO);

			await Task.WhenAll(new Task[] {
				Task.Run(async () => lightBulb.Messages.AddRange(await getMessagesForLightBulb(ID))),
				Task.Run(async () => lightBulb.Quotes.AddRange(await getQuotesForLightBulb(ID))),
				Task.Run(async () => lightBulb.Contacts.AddRange(await getContactsForLightBulb(ID)))
			});

			return lightBulb;
		}

		private async Task<IEnumerable<Message>> getMessagesForLightBulb(int ID)
		{
			var messageAssociations = await database.Table<LightBulbMessageDO>().Where(lbm => lbm.LightBulbDO == ID).ToListAsync();
			var messages = new List<Message>();

			foreach (var messageAssociation in messageAssociations)
			{
				var messageDO = await database.Table<MessageDO>().Where(mDO => mDO.ID == messageAssociation.MessageDO).FirstOrDefaultAsync();
				messages.Add(new Message(messageDO));
			}

			return messages;
		}

		private async Task<IEnumerable<Quote>> getQuotesForLightBulb(int ID)
		{
			var quoteAssociations = await database.Table<LightBulbQuoteDO>().Where(lbq => lbq.LightBulbDO == ID).ToListAsync();
			var quotes = new List<Quote>();

			foreach (var quoteAssocation in quoteAssociations)
			{
				var quoteDO = await database.Table<QuoteDO>().Where(qDO => qDO.ID == quoteAssocation.QuoteDO).FirstOrDefaultAsync();
				quotes.Add(new Quote(quoteDO));
			}

			return quotes;
		}

		private async Task<IEnumerable<Contact>> getContactsForLightBulb(int ID)
		{
			var contactAssociations = await database.Table<LightBulbContactDO>().Where(lbc => lbc.LightBulbDO == ID).ToListAsync();
			var contacts = new List<Contact>();

			foreach (var contactAssocation in contactAssociations)
			{
				var contactDO = await database.Table<ContactDO>().Where(cDO => cDO.ID == contactAssocation.ContactDO).FirstOrDefaultAsync();
				contacts.Add(new Contact(contactDO));
			}

			return contacts;
		}

		public async Task<IEnumerable<LightBulb>> GetAllLightBulbsAsync()
		{
			var dataObjects = await database.Table<LightBulbDO>().ToListAsync();

			var lightBulbs = new List<LightBulb>();
			foreach (var dataObject in dataObjects)
			{
				var lightBulb = new LightBulb(dataObject);
				lightBulbs.Add(lightBulb);
				lightBulb.Messages.AddRange(await getMessagesForLightBulb(lightBulb.ID));
				lightBulb.Quotes.AddRange(await getQuotesForLightBulb(lightBulb.ID));
				lightBulb.Contacts.AddRange(await getContactsForLightBulb(lightBulb.ID));
			}

			return lightBulbs;
		}

		public Task<int> GetContactCountAsync()
		{
			return database.Table<ContactDO>().CountAsync();
		}

		public async Task AddContactAsync(Contact contact)
		{
			var contactDO = new ContactDO(contact);
			await database.InsertAsync(contactDO);
			contact.ID = contactDO.ID;
		}

		public Task RemoveContactAsync(Contact contact)
		{
			var contactDO = new ContactDO(contact);
			return database.DeleteAsync(contactDO);
		}

		public async Task<IEnumerable<Contact>> GetAllContactsAsync()
		{
			var dataObjects = await database.Table<ContactDO>().ToListAsync();

			var contacts = new List<Contact>();
			foreach (var dataObject in dataObjects)
			{
				contacts.Add(new Contact(dataObject));
			}

			return contacts;
		}

		public Task<int> GetMessageCountAsync()
		{
			return database.Table<MessageDO>().CountAsync();
		}

		public async Task AddMessageAsync(Message message)
		{
			var messageDO = new MessageDO(message);
			await database.InsertAsync(messageDO);
			message.ID = messageDO.ID;
		}

		public Task RemoveMessageAsync(Message message)
		{
			var messageDO = new MessageDO(message);
			return database.DeleteAsync(messageDO);
		}

		public async Task<IEnumerable<Message>> GetAllMessagesAsync()
		{
			var dataObjects = await database.Table<MessageDO>().ToListAsync();

			var messages = new List<Message>();
			foreach (var dataObject in dataObjects)
			{
				messages.Add(new Message(dataObject));
			}

			return messages;
		}

		public Task<int> GetQuoteCountAsync()
		{
			return database.Table<QuoteDO>().CountAsync();
		}

		public async Task AddQuoteAsync(Quote quote)
		{
			var quoteDO = new QuoteDO(quote);
			await database.InsertAsync(quoteDO);
			quote.ID = quoteDO.ID;
		}

		public Task RemoveQuoteAsync(Quote quote)
		{
			var quoteDO = new QuoteDO(quote);
			return database.DeleteAsync(quoteDO);
		}

		public async Task<IEnumerable<Quote>> GetAllQuotesAsync()
		{
			var dataObjects = await database.Table<QuoteDO>().ToListAsync();

			var quotes = new List<Quote>();
			foreach (var dataObject in dataObjects)
			{
				quotes.Add(new Quote(dataObject));
			}

			return quotes;
		}

		public Task AssociateLightBulbWithMessageAsync(LightBulb lightBulb, Message message)
		{
			var lightBulbDO = new LightBulbDO(lightBulb);
			var messageDO = new MessageDO(message);
			var lightBulbMessageDO = new LightBulbMessageDO
			{
				LightBulbDO = lightBulbDO.ID,
				MessageDO = messageDO.ID
			};

			return database.InsertAsync(lightBulbMessageDO);
		}

		public Task AssociateLightBulbWithQuoteAsync(LightBulb lightBulb, Quote quote)
		{
			var lightBulbDO = new LightBulbDO(lightBulb);
			var quoteDO = new QuoteDO(quote);
			var lightBulbQuoteDO = new LightBulbQuoteDO
			{
				LightBulbDO = lightBulbDO.ID,
				QuoteDO = quoteDO.ID
			};

			return database.InsertAsync(lightBulbQuoteDO);
		}

		public Task AssociateLightBulbWithContactAsync(LightBulb lightBulb, Contact contact)
		{
			var lightBulbDO = new LightBulbDO(lightBulb);
			var contactDO = new ContactDO(contact);
			var lightBulbConactDO = new LightBulbContactDO
			{
				LightBulbDO = lightBulbDO.ID,
				ContactDO = contactDO.ID
			};

			return database.InsertAsync(lightBulbConactDO);
		}
	}
}
