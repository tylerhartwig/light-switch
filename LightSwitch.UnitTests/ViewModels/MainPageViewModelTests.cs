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
		MainPageViewModel mainPageViewModel;

		public MainPageViewModelTests()
		{
			navigationService = new NavigationServiceMock();
			mainPageViewModel = new MainPageViewModel(navigationService);
			navigationService.AssociateViewModelForView<AddEditLightBulbViewModel, Page>();
		}

		[Fact]
		public void TestLightBulbsProperty()
		{
			var result = false;
			mainPageViewModel.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == "LightBulbs")
				{
					result = true;
				}
			};

			mainPageViewModel.LightBulbs = new ObservableCollection<LightBulbViewModel>();
			Assert.True(result, "PropertyChanged was not raised for \"LightBulbs\"");
		}

		[Fact]
		public void TestAddLightBulbProperty()
		{
			var propertyChanged = false;

			mainPageViewModel.PropertyChanged += (sender, e) =>
			{
				propertyChanged = true;
			};

			mainPageViewModel.AddLightBulb = new AwaitableCommand(async () => { });
			Assert.True(propertyChanged, "PropertyChanged was not raised for property \"AddLightBulb\"");
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
	}
}
