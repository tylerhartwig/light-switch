using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Ninject;

namespace LightSwitch
{
	public class MainPageViewModel : BaseViewModel
	{
		private ObservableCollection<LightBulbViewModel> lightBulbs;
		public ObservableCollection<LightBulbViewModel> LightBulbs
		{
			get { return lightBulbs; }
			set
			{
				SetProperty(ref lightBulbs, value);
			}
		}

		private IAsyncCommand _addLightBulb;
		public IAsyncCommand AddLightBulb
		{
			get { return _addLightBulb; }
			set
			{
				SetProperty(ref _addLightBulb, value);
			}
		}

		private IAsyncCommand _refreshLightBulbs;
		public IAsyncCommand RefreshLightBulbs
		{
			get { return _refreshLightBulbs; }
			set
			{
				SetProperty(ref _refreshLightBulbs, value);
			}
		}

		private INavigationService navigationService;
		private IDatabaseService databaseService;

		public MainPageViewModel(INavigationService navigationService, IDatabaseService databaseService)
		{
			this.navigationService = navigationService;
			this.databaseService = databaseService;
			LightBulbs = new ObservableCollection<LightBulbViewModel>();
			AddLightBulb = new AwaitableCommand(addLightBulb);
			RefreshLightBulbs = new AwaitableCommand(refreshLightBulbs);
		}

		private async Task addLightBulb()
		{
			LightBulbs.Add(new LightBulbViewModel());
			await navigationService.GoToPageForViewModel<AddEditLightBulbViewModel>();
		}

		private async Task refreshLightBulbs()
		{
			var lightBulbs = await databaseService.GetAllLightBulbsAsync();

			LightBulbs.Clear();
			foreach (var lightBulb in lightBulbs)
			{
				LightBulbs.Add(new LightBulbViewModel(lightBulb));
			}
		}
	}
}
