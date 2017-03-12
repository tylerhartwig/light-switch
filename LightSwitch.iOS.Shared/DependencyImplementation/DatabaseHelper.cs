using System;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(LightSwitch.iOS.DatabaseHelper))]
namespace LightSwitch.iOS
{
	public class DatabaseHelper : IDatabaseHelper
	{
		private readonly string databasesFolder;

		public DatabaseHelper()
		{
			var documentFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			databasesFolder = Path.Combine(documentFolder, "..", "Library", "Databases");
		}

		public string GetDatabasePath(string filename)
		{
			if (!Directory.Exists(databasesFolder))
			{
				Directory.CreateDirectory(databasesFolder);
			}

			string filePath = Path.Combine(databasesFolder, filename);

			return filePath;
		}
	}
}
