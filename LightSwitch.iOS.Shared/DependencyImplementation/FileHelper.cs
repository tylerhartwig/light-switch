using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(LightSwitch.iOS.FileHelper))]
namespace LightSwitch.iOS
{
	public class FileHelper : IFileHelper
	{
		public FileHelper()
		{
		}

		public string GetDatabasePath(string filename)
		{
			string documentFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string databasesFolder = Path.Combine(documentFolder, "..", "Library", "Databases");

			if (!Directory.Exists(databasesFolder))
			{
				Directory.CreateDirectory(databasesFolder);
			}

			return Path.Combine(databasesFolder, filename);
		}
	}
}
