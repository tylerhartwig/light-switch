using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ninject;
using Xamarin.Forms;

namespace LightSwitch
{
	public class NavigationService : INavigationService
	{
		public BaseViewModel CurrentViewModel
		{
			get
			{
				return (BaseViewModel)navigationPage.CurrentPage.BindingContext;
			}
		}

		public Page CurrentPage
		{
			get
			{
				if (navigationPage.CurrentPage != null)
				{
					return navigationPage.CurrentPage;
				}
				else
				{
					return navigationPage;
				}
			}
		}

		private NavigationPage navigationPage;
		private IDictionary<Type, Type> viewModelAssociations = new Dictionary<Type, Type>();

		public NavigationService(NavigationPage navigationPage)
		{
			this.navigationPage = navigationPage;
		}

		public NavigationPage Bootstrap()
		{
			return navigationPage;
		}

		public Task GoBack()
		{
			return navigationPage.PopAsync();
		}

		public Task<T> GoToPageForViewModel<T>() where T : BaseViewModel
		{
			var tcs = new TaskCompletionSource<T>();

			var pageType = viewModelAssociations[typeof(T)];
			var page = (Page)App.Container.Get(pageType);
			var viewModel = App.Container.Get<T>();
			page.BindingContext = viewModel;
			Device.BeginInvokeOnMainThread(async () =>
			{
				await navigationPage.PushAsync(page);
				tcs.SetResult(viewModel);
			});
			return tcs.Task;
		}

		public void AssociateViewModelForView<T1, T2>() where T1 : BaseViewModel where T2 : Page
		{
			viewModelAssociations[typeof(T1)] = typeof(T2);
		}
	}
}
