using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightSwitch
{
	public interface IDatabaseService
	{
		Task InitializeAsync();
		Task ResetDatabaseAsync();

		#region LightBulb
		Task<int> GetLightBulbCountAsync();
		Task AddLightBulbAsync(LightBulb lightBulb);
		Task RemoveLightBulbAsync(LightBulb lightBulb);
		Task<LightBulb> GetLightBulbAsync(int ID);
		Task<IEnumerable<LightBulb>> GetAllLightBulbsAsync();
		#endregion

		#region Contact
		Task<int> GetContactCountAsync();
		Task AddContactAsync(Contact contact);
		Task RemoveContactAsync(Contact contact);
		Task<IEnumerable<Contact>> GetAllContactsAsync();
		#endregion

		#region Message
		Task<int> GetMessageCountAsync();
		Task AddMessageAsync(Message message);
		Task RemoveMessageAsync(Message message);
		Task<IEnumerable<Message>> GetAllMessagesAsync();
		#endregion

		#region Quote
		Task<int> GetQuoteCountAsync();
		Task AddQuoteAsync(Quote quote);
		Task RemoveQuoteAsync(Quote quote);
		Task<IEnumerable<Quote>> GetAllQuotesAsync();
		#endregion

		#region Associations 
		Task AssociateLightBulbWithMessageAsync(LightBulb lightBulb, Message message);
		Task AssociateLightBulbWithQuoteAsync(LightBulb lightBulb, Quote quote);
		Task AssociateLightBulbWithContactAsync(LightBulb lightBulb, Contact contact);
		#endregion
	}
}
