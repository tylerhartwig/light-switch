using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class NavigationServiceMockTests 
	{
		public class ViewModel1 : BaseViewModel { }
		public class ViewModel2 : BaseViewModel { }
		public class Page1 : Page { }
		public class Page2 : Page { }

		protected INavigationService service;

		public NavigationServiceMockTests()
		{
			service = new NavigationServiceMock();
		}

		[Fact]
		public async Task TestGoToPageForViewModelWithoutRegistration()
		{
			var exceptionCaught = false;

			try
			{
				await service.GoToPageForViewModel<ViewModel1>();
			}
			catch (KeyNotFoundException)
			{
				exceptionCaught = true;
			}

			Assert.True(exceptionCaught, "Exception was not thrown for navigating to an unregistered viewmodel");
		}

		[Fact]
		public async Task TestGoToPageForViewModelWithRegistration()
		{
			service.AssociateViewModelForView<ViewModel1, Page1>();
			var vm = await service.GoToPageForViewModel<ViewModel1>();
			Assert.True(vm is ViewModel1, "Returned view model was not the expected view model");
			Assert.Same(vm, service.CurrentViewModel);
			Assert.True(service.CurrentPage is Page1, string.Format("Current Page ({0}) is not the expected current page", service.CurrentPage));
		}

		[Fact]
		public async Task TestGoBack()
		{
			service.AssociateViewModelForView<ViewModel1, Page1>();
			service.AssociateViewModelForView<ViewModel2, Page2>();

			var vm1 = await service.GoToPageForViewModel<ViewModel1>();
			var vm2 = await service.GoToPageForViewModel<ViewModel2>();

			Assert.Same(vm2, service.CurrentViewModel);
			Assert.True(service.CurrentPage is Page2, string.Format("Current Page ({0}) is not the expected current page", service.CurrentPage));

			await service.GoBack();
			Assert.Same(vm1, service.CurrentViewModel);
			Assert.True(service.CurrentPage is Page1, string.Format("Current Page ({0}) is not the expected current page", service.CurrentPage));
		}
	}
}
