using System;
using System.Collections.ObjectModel;

namespace LightSwitch
{
	public class LightBulbViewModel : BaseViewModel
	{
		private LightBulb lightBulb;

		public string Name
		{
			get { return lightBulb.Name; }
			set
			{
				lightBulb.Name = value;
				OnPropertyChanged();
			}
		}

		private ObservableCollection<MessageViewModel> messages;
		public ObservableCollection<MessageViewModel> Messages
		{
			get { return messages; }
			set
			{
				SetProperty(ref messages, value);
			}
		}

		private ObservableCollection<ContactViewModel> contacts;
		public ObservableCollection<ContactViewModel> Contacts
		{
			get { return contacts; }
			set
			{
				SetProperty(ref contacts, value);
			}
		}

		private ObservableCollection<QuoteViewModel> quotes;
		public ObservableCollection<QuoteViewModel> Quotes
		{
			get { return quotes; }
			set
			{
				SetProperty(ref quotes, value);
			}
		}

		public LightBulbViewModel() 
		{
			lightBulb = new LightBulb();
			Messages = new ObservableCollection<MessageViewModel>();
			Contacts = new ObservableCollection<ContactViewModel>();
			Quotes = new ObservableCollection<QuoteViewModel>();
		}

		public LightBulbViewModel(LightBulb lightBulb) : this()
		{
			this.lightBulb = lightBulb;

			foreach (var message in lightBulb.Messages)
			{
				Messages.Add(new MessageViewModel(message));
			}

			foreach (var contact in lightBulb.Contacts)
			{
				var contactViewModel = new ContactViewModel();
				contactViewModel.Contact = contact;
				Contacts.Add(contactViewModel);
			}

			foreach (var quote in lightBulb.Quotes)
			{
				Quotes.Add(new QuoteViewModel(quote));
			}
		}
	}
}
