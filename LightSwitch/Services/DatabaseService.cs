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
			return database.CreateTablesAsync<LightBulbDO, ContactDO, MessageDO>();
		}

		public Task ResetDatabaseAsync()
		{
			return Task.WhenAll(new Task[] {
				database.DropTableAsync<LightBulbDO>(),
				database.DropTableAsync<ContactDO>(),
				database.DropTableAsync<MessageDO>()
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

		public Task RemoveLightBulbAsync(LightBulb lightBulb)
		{
			var lightBulbDO = new LightBulbDO(lightBulb);
			return database.DeleteAsync(lightBulbDO);
		}

		public async Task<IEnumerable<LightBulb>> GetAllLightBulbsAsync()
		{
			var dataObjects = await database.Table<LightBulbDO>().ToListAsync();

			var lightBulbs = new List<LightBulb>();
			foreach (var dataObject in dataObjects)
			{
				lightBulbs.Add(new LightBulb(dataObject));
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
	}
}
