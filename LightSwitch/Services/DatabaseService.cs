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
				database.CreateTableAsync<LightBulbDO>()
			});
		}

		public Task ResetDatabaseAsync()
		{
			return Task.WhenAll(new Task[] {
				database.DropTableAsync<LightBulbDO>()
			});
		}

		public Task<int> GetLightBulbCountAsync()
		{
			return database.Table<LightBulbDO>().CountAsync();
		}

		public Task AddLightBulbAsync(LightBulbDO lightBulb)
		{
			return database.InsertAsync(lightBulb);
		}

		public Task RemoveLightBulbAsync(LightBulbDO lightBulb)
		{
			return database.DeleteAsync(lightBulb);
		}

		public Task<List<LightBulbDO>> GetAllLightBulbsAsync()
		{
			return database.Table<LightBulbDO>().ToListAsync();
		}
	}
}
