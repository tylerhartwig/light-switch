using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Contacts;

namespace LightSwitch
{
	public class AddressBookService : IAddressBookService
	{
		private bool hasPermission = false;

		public async Task<bool> RequestPermission()
		{
			hasPermission = await CrossContacts.Current.RequestPermission();
			return hasPermission;
		}		

		public async Task<IEnumerable<string>> GetContactNames()
		{
			if (!hasPermission)
			{
				throw new UnauthorizedAccessException("Permission has not been granted to access the Address Book");
			}

			List<Plugin.Contacts.Abstractions.Contact> contacts = null;
			await Task.Run(() =>
			{
				contacts = CrossContacts.Current.Contacts.Where(c => c.Phones.Count > 0 &&
				                                                    (!string.IsNullOrWhiteSpace(c.FirstName) || !string.IsNullOrWhiteSpace(c.LastName)))
				                            				 .OrderBy(c => c.FirstName)
				                            			 	 .ToList();
			});

			var names = new List<string>();
			foreach (var contact in contacts)
			{
				names.Add(string.Format("{0} {1}", contact.FirstName, contact.LastName));
			}

			return names;
		}

	}
}
