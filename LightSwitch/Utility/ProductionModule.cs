using System;
using Ninject.Modules;
using Xamarin.Forms;

namespace LightSwitch
{
	public class ProductionModule : NinjectModule
	{
		public override void Load()
		{
			Bind<INavigationService>().To<NavigationService>().InSingletonScope();
			Bind<IAddressBookService>().To<AddressBookService>().InSingletonScope();
			Bind<IDatabaseService>().To<DatabaseService>().InSingletonScope();
			Bind<NavigationPage>().ToSelf().WithConstructorArgument<Page>(new MainPage());
		}
	}
}
