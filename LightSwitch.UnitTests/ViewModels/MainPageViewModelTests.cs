using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class MainPageViewModelTests 
	{
		INavigationService navigationService;
		IDatabaseService databaseService;
		MainPageViewModel mainPageViewModel;

		public MainPageViewModelTests()
		{
			navigationService = new NavigationServiceMock();
			databaseService = new DatabaseServiceMock();
			mainPageViewModel = new MainPageViewModel(navigationService, databaseService);
			navigationService.AssociateViewModelForView<AddEditLightBulbViewModel, Page>();
		}

		[Fact]
		public void TestPropertyChangedEvents()
		{
			TestHelper.TestPropertyChanged(mainPageViewModel, nameof(mainPageViewModel.LightBulbs), new ObservableCollection<LightBulbViewModel>());
			TestHelper.TestPropertyChanged(mainPageViewModel, nameof(mainPageViewModel.AddLightBulb), AwaitableCommand.Empty);
			TestHelper.TestPropertyChanged(mainPageViewModel, nameof(mainPageViewModel.RefreshLightBulbs), AwaitableCommand.Empty);
		}

		[Fact]
		public async Task TestAddLightBulb()
		{
			var beforeCount = mainPageViewModel.LightBulbs.Count;

			var addLightBulbCommand = mainPageViewModel.AddLightBulb;
			navigationService.AssociateViewModelForView<AddEditLightBulbViewModel, ContentPage>();
			await addLightBulbCommand.ExecuteAsync(null);
			var afterCount = mainPageViewModel.LightBulbs.Count;
			Assert.Equal(beforeCount + 1, afterCount);

			var currentViewModel = navigationService.CurrentViewModel;
			Assert.True(currentViewModel is AddEditLightBulbViewModel, "AddLightBulb did not change to correct view model");
		}

		[Fact]
		public async Task TestRefreshLightBulbs()
		{
			var lightBulb = new LightBulb();
			await databaseService.AddLightBulbAsync(lightBulb);

			var refreshCommand = mainPageViewModel.RefreshLightBulbs;
			await refreshCommand.ExecuteAsync(null);

			Assert.Equal(await databaseService.GetLightBulbCountAsync(), mainPageViewModel.LightBulbs.Count);
		}
	}
}
