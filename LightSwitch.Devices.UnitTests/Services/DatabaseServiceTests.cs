using System;
using LightSwitch.UnitTests;
using Ninject;

namespace LightSwitch.Devices.UnitTests
{
	public class DatabaseServiceTests : DatabaseServiceMockTests
	{
		public DatabaseServiceTests()
		{
			service = new DatabaseService(App.Container.Get<IDatabaseHelper>());
			service.InitializeAsync().Wait();
		}
	}
}
