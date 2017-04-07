using System;
using Xamarin.Forms;

namespace LightSwitch.UnitTests
{
	public class NavigationServiceTests : NavigationServiceMockTests
	{
		public NavigationServiceTests()
		{
			service = new NavigationService(new NavigationPage());
		}
	}
}
