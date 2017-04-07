using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightSwitch
{
	public interface IAddressBookService
	{
		Task<bool> RequestPermission();
		Task<IEnumerable<string>> GetContactNames();
	}
}
