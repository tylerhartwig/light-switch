using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LightSwitch
{
	public interface INavigationService
	{
		BaseViewModel CurrentViewModel { get; }
		Page CurrentPage { get; }
		Task<T> GoToPageForViewModel<T>() where T : BaseViewModel;
		Task GoBack();
		void AssociateViewModelForView<T1, T2>() where T1 : BaseViewModel where T2 : Page;
	}
}
