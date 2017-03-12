using System;
namespace LightSwitch
{
	public interface IDatabaseHelper
	{
		string GetDatabasePath(string filename);
		//void DeleteDatabase(string filename);
	}
}
