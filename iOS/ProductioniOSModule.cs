using System;
using Ninject.Modules;

namespace LightSwitch.iOS
{
	public class ProductioniOSModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IDatabaseHelper>().To<DatabaseHelper>().InSingletonScope();
		}
	}
}
