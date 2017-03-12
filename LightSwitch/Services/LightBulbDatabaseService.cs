using System;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace LightSwitch
{
	public class LightBulbDatabaseService
	{
		private static LightBulbDatabaseService instance;
		private readonly SQLiteAsyncConnection database;

		public static LightBulbDatabaseService Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new LightBulbDatabaseService();
				}
				return instance;
			}
		}

		private LightBulbDatabaseService()
		{
			var dbPath = DependencyService.Get<IFileHelper>().GetDatabasePath("LightBulbSQLite.db3");
			database = new SQLiteAsyncConnection(dbPath);
			database.CreateTableAsync<LightBulb>().Wait();
		}

		public Task<int> GetLightBulbCountAsync()
		{
			return database.Table<LightBulb>().CountAsync();
		}

		public Task AddLightBulbAsync(LightBulb lightBulb)
		{
			return database.InsertAsync(lightBulb);
		}

	}
}
