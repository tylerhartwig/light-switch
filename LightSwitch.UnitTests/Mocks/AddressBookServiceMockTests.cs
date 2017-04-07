using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class AddressBookServiceMockTests 
	{
		protected IAddressBookService service;

		public AddressBookServiceMockTests()
		{
			service = new AddressBookServiceMock();
		}

		[Fact]
		public async Task TestGetNamesWithoutPermission()
		{
			var exceptionCaught = false;
			IEnumerable<string> names = null;

			try
			{
				names = await service.GetContactNames();
			}
			catch (UnauthorizedAccessException)
			{
				exceptionCaught = true;
			}
			Assert.True(exceptionCaught, "Attempting to get contacts without permission did not throw an exception");
			Assert.Null(names);
		}

		[Fact]
		public async Task TestGetNamesWithPermission()
		{
			var exceptionCaught = false;
			IEnumerable<string> names = null;

			var permission = await service.RequestPermission();
			Assert.True(permission, "Test failed to get permission to access contacts");

			try
			{
				names = await service.GetContactNames();
			}
			catch (UnauthorizedAccessException)
			{
				exceptionCaught = true;
			}

			Assert.False(exceptionCaught, "Exception was thrown even though permission was granted");
			Assert.NotNull(names);
		}
	}
}
