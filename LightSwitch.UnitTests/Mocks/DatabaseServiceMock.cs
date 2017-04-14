using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightSwitch.UnitTests
{
	public class DatabaseServiceMock : IDatabaseService
	{
		private List<LightBulb> lightBulbs = new List<LightBulb>();
		private List<Contact> contacts = new List<Contact>();
		private List<Message> messages = new List<Message>();
		private List<Quote> quotes = new List<Quote>();

		private static readonly object generalLock = new object();

		public DatabaseServiceMock() { }

		public Task InitializeAsync()
		{
			return Task.Run(() => { });
		}

		public Task ResetDatabaseAsync()
		{
			return Task.Run(() => 
			{
				lock(generalLock)
				{
					lightBulbs.Clear();
					contacts.Clear();
					messages.Clear();
					quotes.Clear();
				}
			});
		}


		#region LightBulb


		public Task<int> GetLightBulbCountAsync()
		{
			return Task.Run(() =>
			{
				lock(generalLock)
				{
					return lightBulbs.Count;
				}
			});
		}

		public Task AddLightBulbAsync(LightBulb lightBulb)
		{
			return Task.Run(() =>
			{
				lock(generalLock)
				{
					lightBulbs.Add(lightBulb);
					lightBulb.ID = lightBulbs.IndexOf(lightBulb);
				}
			});
		}

		public Task RemoveLightBulbAsync(LightBulb lightBulb)
		{
			return Task.Run(() =>
			{
				lock(generalLock)
				{
					lightBulbs.Remove(lightBulb);
				}
			});
		}

		public Task<LightBulb> GetLightBulbAsync(int ID)
		{
			return Task.Run(() =>
			{
				lock(generalLock)
				{
					return lightBulbs[ID];
				}
			});
		}

		public Task<IEnumerable<LightBulb>> GetAllLightBulbsAsync()
		{
			return Task.Run(() =>
			{
				lock(generalLock)
				{
					return (IEnumerable<LightBulb>)lightBulbs;
				}
			});
		}

		#endregion

		#region Contact

		public Task<int> GetContactCountAsync()
		{
			return Task.Run(() => 
			{ 
				lock(generalLock)
				{
					return contacts.Count;
				}
			});
		}

		public Task AddContactAsync(Contact contact)
		{
			return Task.Run(() =>
			{
				lock(generalLock)
				{
					contacts.Add(contact);
				}
			});
		}

		public Task RemoveContactAsync(Contact contact)
		{
			return Task.Run(() =>
			{
				lock(generalLock)
				{
					contacts.Remove(contact);
				}
			});
		}

		public Task<IEnumerable<Contact>> GetAllContactsAsync()
		{
			return Task.Run(() => 
			{ 
				lock(generalLock)
				{
					return (IEnumerable<Contact>)contacts;
				}
			});
		}

		#endregion

		#region Message

		public Task<int> GetMessageCountAsync()
		{
			return Task.Run(() => 
			{ 
				lock(generalLock)
				{
					return messages.Count;
				}
			});
		}

		public Task AddMessageAsync(Message message)
		{
			return Task.Run(() =>
			{
				lock(generalLock)
				{
					messages.Add(message);
				}
			});
		}

		public Task RemoveMessageAsync(Message message)
		{
			return Task.Run(() =>
			{
				lock(generalLock)
				{
					messages.Remove(message);
				}
			});
		}

		public Task<IEnumerable<Message>> GetAllMessagesAsync()
		{
			return Task.Run(() => 
			{ 
				lock(generalLock)
				{
					return (IEnumerable<Message>)messages;
				}
			});
		}

		#endregion

		#region Quote

		public Task<int> GetQuoteCountAsync()
		{
			return Task.Run(() =>
			{
				lock(generalLock)
				{
					return quotes.Count;
				}
			});
		}

		public Task AddQuoteAsync(Quote quote)
		{
			return Task.Run(() =>
			{
				lock(generalLock)
				{
					quotes.Add(quote);
				}
			});
		}

		public Task RemoveQuoteAsync(Quote quote)
		{
			return Task.Run(() =>
			{
				lock(generalLock)
				{
					quotes.Remove(quote);
				}
			});
		}

		public Task<IEnumerable<Quote>> GetAllQuotesAsync()
		{
			return Task.Run(() =>
			{
				lock(generalLock)
				{
					return (IEnumerable<Quote>)quotes;
				}
			});
		}

		#endregion

		#region Assocations

		public Task AssociateLightBulbWithContactAsync(LightBulb lightBulb, Contact contact)
		{
			return Task.Run(() =>
			{
				lock(generalLock)
				{
					lightBulb.Contacts.Add(contact);
				}
			});
		}

		public Task AssociateLightBulbWithMessageAsync(LightBulb lightBulb, Message message)
		{
			return Task.Run(() =>
			{
				lock(generalLock)
				{
					lightBulb.Messages.Add(message);
				}
			});
		}

		public Task AssociateLightBulbWithQuoteAsync(LightBulb lightBulb, Quote quote)
		{
			return Task.Run(() =>
			{
				lock(generalLock)
				{
					lightBulb.Quotes.Add(quote);
				}
			});
		}

		#endregion

	}
}
