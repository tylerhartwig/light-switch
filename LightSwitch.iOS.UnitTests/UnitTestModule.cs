using System;
using LightSwitch.UnitTests;
using Ninject.Modules;

namespace LightSwitch.iOS.UnitTests
{
	public class UnitTestModule : NinjectModule
	{
		public override void Load()
		{
			Bind<INavigationService>().To<NavigationServiceMock>();
			Bind<IAddressBookService>().To<AddressBookServiceMock>();
			Bind<IDatabaseHelper>().To<DatabaseHelper>().InSingletonScope();
		}
	}
}
