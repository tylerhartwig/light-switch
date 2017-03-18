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
			return Task.WhenAll(new Task[]{
				database.CreateTableAsync<LightBulbDO>(),
				database.CreateTableAsync<ContactDO>()
			});
		}

		public Task ResetDatabaseAsync()
		{
			return Task.WhenAll(new Task[] {
				database.DropTableAsync<LightBulbDO>(),
				database.DropTableAsync<ContactDO>()
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

		public async Task RemoveContactAsync(Contact contact)
		{
			var contactDO = new ContactDO(contact);
			await database.DeleteAsync(contactDO);
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
	}
}
