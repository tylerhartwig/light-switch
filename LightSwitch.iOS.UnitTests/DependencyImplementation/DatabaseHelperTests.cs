using System;
using System.IO;
using Xunit;

namespace LightSwitch.iOS.UnitTests
{
	public class DatabaseHelperTests
	{
		[Fact]
		public void TestGetDatabasePath()
		{
			var fileHelper = new DatabaseHelper();

			var databaseName = "database.db";
			var filePath = Path.Combine("Library", "Databases", databaseName);

			var returnedPath = fileHelper.GetDatabasePath(databaseName);
			var createdDirectory = returnedPath.Replace(databaseName, "");
			Assert.True(returnedPath.EndsWith(filePath, StringComparison.CurrentCulture));
			Assert.True(Directory.Exists(createdDirectory));
		}
	}
}
