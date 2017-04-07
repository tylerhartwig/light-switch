using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightSwitch.UnitTests
{
	public class AddressBookServiceMock : IAddressBookService
	{
		private bool hasPermission = false;

		Dictionary<string, string> addressBook = new Dictionary<string, string>();

		public AddressBookServiceMock()
		{
			addressBook.Add("Tyler Hartwig", "6475217446");
			addressBook.Add("Bobby Baylor", "0123456789");
		}

		public async Task<bool> RequestPermission()
		{
			return await Task.Run(() => { hasPermission = true; return hasPermission; });
		}

		public async Task<IEnumerable<string>> GetContactNames()
		{
			if (!hasPermission)
			{
				throw new UnauthorizedAccessException("Permission has not been granted to access the Address Book");
			}
			return await Task.Run(() => addressBook.Keys.ToList());
		}
	}
}
