using System;
using System.IO;
using NUnit.Framework;

namespace LightSwitch.iOS.UnitTests
{
	[TestFixture]
	public class FileHelperTests
	{
		[Test]
		public void TestGetDatabasePath()
		{
			var fileHelper = new FileHelper();

			var databaseName = "database.db";
			var filePath = Path.Combine("Library", "Databases", databaseName);

			var returnedPath = fileHelper.GetDatabasePath(databaseName);
			var createdDirectory = returnedPath.Replace(databaseName, "");
			Assert.True(returnedPath.EndsWith(filePath, StringComparison.CurrentCulture));
			Assert.True(Directory.Exists(createdDirectory));
		}

		[Test]
		public void TestDeleteDatabase()
		{
		}
	}
}
