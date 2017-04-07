using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LightSwitch.UnitTests
{
	public class NavigationServiceMock : INavigationService
	{
		private IDictionary<Type, Type> viewModelAssociations = new Dictionary<Type, Type>();

		public BaseViewModel CurrentViewModel
		{
			get
			{
				return viewModelStack.Peek();
			}
		}

		public Page CurrentPage
		{
			get
			{
				return pageStack.Peek();
			}
		}

		private Stack<BaseViewModel> viewModelStack = new Stack<BaseViewModel>();
		private Stack<Page> pageStack = new Stack<Page>();

		private class viewModel : BaseViewModel { }

		public NavigationServiceMock() 
		{
			// Simulate a root page
			viewModelStack.Push(new viewModel());
			pageStack.Push(new Page());
		}

		public Task<T> GoToPageForViewModel<T>() where T : BaseViewModel
		{
			return Task<T>.Run(() =>
			{
				var viewModel = (T)Activator.CreateInstance(typeof(T), createNullParams(typeof(T)));
				viewModelStack.Push(viewModel);
				var pageType = viewModelAssociations[viewModel.GetType()];
				pageStack.Push((Page)Activator.CreateInstance(pageType));
				return viewModel;
			});
		}

		private object[] createNullParams(Type type)
		{
			var constructor = type.GetTypeInfo().DeclaredConstructors.FirstOrDefault();
			var numParams = constructor.GetParameters().Length;
			var paramList = new List<object>();

			for (int i = 0; i < numParams; i++)
			{
				paramList.Add(null);
			}

			return paramList.ToArray();
		}

		public Task GoBack()
		{
			return Task.Run(() =>
			{
				viewModelStack.Pop();
				pageStack.Pop();
			});
		}

		public void AssociateViewModelForView<T1, T2>() where T1 : BaseViewModel where T2 : Page
		{
			viewModelAssociations[typeof(T1)] = typeof(T2);
		}
	}
}
