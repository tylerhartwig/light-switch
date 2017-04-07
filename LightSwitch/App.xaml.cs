using Ninject;
using Xamarin.Forms;

namespace LightSwitch
{
	public partial class App : Application
	{
		public static IKernel Container { get; set; }

		public App()
		{
			Initialize();
			InitializeComponent();

			var navService = Container.Get<INavigationService>();
			AssociateViewModels(navService);
			var navPage = ((NavigationService)navService).Bootstrap();
			navPage.CurrentPage.BindingContext = Container.Get<MainPageViewModel>();
			MainPage = navPage;
		}

		public static void Initialize()
		{
			var kernel = new StandardKernel(new ProductionModule());
			Container = kernel;
		}

		public static void AssociateViewModels(INavigationService service)
		{
			service.AssociateViewModelForView<MainPageViewModel, MainPage>();
			service.AssociateViewModelForView<AddEditLightBulbViewModel, AddEditLightBulbPage>();
			service.AssociateViewModelForView<AddContactViewModel, AddContactPage>();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
