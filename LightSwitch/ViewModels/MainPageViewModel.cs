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

		private INavigationService navigationService;

		public MainPageViewModel(INavigationService navigationService)
		{
			this.navigationService = navigationService;
			LightBulbs = new ObservableCollection<LightBulbViewModel>();
			AddLightBulb = new AwaitableCommand(async () => { await addLightBulb(); });
		}

		private async Task addLightBulb()
		{
			LightBulbs.Add(new LightBulbViewModel());
			await navigationService.GoToPageForViewModel<AddEditLightBulbViewModel>();
		}
	}
}
