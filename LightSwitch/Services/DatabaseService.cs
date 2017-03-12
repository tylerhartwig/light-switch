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
				database.CreateTableAsync<LightBulb>()
			});
		}

		public Task ResetDatabaseAsync()
		{
			return Task.WhenAll(new Task[] {
				database.DropTableAsync<LightBulb>()
			});
		}

		public Task<int> GetLightBulbCountAsync()
		{
			return database.Table<LightBulb>().CountAsync();
		}

		public Task AddLightBulbAsync(LightBulb lightBulb)
		{
			return database.InsertAsync(lightBulb);
		}

		public Task RemoveLightBulbAsync(LightBulb lightBulb)
		{
			return database.DeleteAsync(lightBulb);
		}

		public Task<List<LightBulb>> GetAllLightBulbsAsync()
		{
			return database.Table<LightBulb>().ToListAsync();
		}
	}
}
