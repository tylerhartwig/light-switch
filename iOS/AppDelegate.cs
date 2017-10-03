using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Ninject;
using Ninject.Modules;
using UIKit;

namespace LightSwitch.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			// Code for starting up the Xamarin Test Cloud Agent
#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
#endif
			App.Container = new StandardKernel(new INinjectModule[] { new ProductionModule(), new ProductioniOSModule() });
			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}

#if ENABLE_TEST_CLOUD
		[Export("resetDatabase:")]
		public NSString resetDatabase(NSString value)
		{
			var databaseService = App.Container.Get<IDatabaseService>();
			databaseService.ResetDatabaseAsync().Wait();
			databaseService.InitializeAsync().Wait();
			return new NSString("0");
		}

		[Export("addLightBulb:")]
		public NSString addLightBulb(NSString value)
		{
			var lightBulb = new LightBulb { Name = value };
			var databaseService = App.Container.Get<IDatabaseService>();
			databaseService.InitializeAsync().Wait();
			databaseService.AddLightBulbAsync(lightBulb).Wait();

			return new NSString("0");
		}
#endif
	}
}
