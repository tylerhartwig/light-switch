using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class LightBulbViewModelTests
	{
		[Fact]
		public void TestDefaultConstructor()
		{
			var lightBulbViewModel = new LightBulbViewModel();
			Assert.NotNull(lightBulbViewModel);
		}

		[Fact]
		public void TestLightBulbConstructor()
		{
			var lightBulb = new LightBulb
			{
				ID = 777,
				Name = "test name"
			};

			var messages = new List<Message>();
			var contacts = new List<Contact>();
			var quotes = new List<Quote>();
			messages.Add(new Message
			{
				ID = 888,
				Text = "test"
			});
			messages.Add(new Message
			{
				ID = 999,
				Text = "Some text or something"
			});

			contacts.Add(new Contact
			{
				ID = 111,
				Name = "Billy"
			});
			contacts.Add(new Contact
			{
				ID = 7,
				Name = "Bobby"
			});
			contacts.Add(new Contact
			{
				ID = 22,
				Name = "Tyler"
			});

			quotes.Add(new Quote
			{
				ID = 444,
				Text = "quote text",
				Reference = "the author"
			});

			lightBulb.Messages = messages;
			lightBulb.Contacts = contacts;
			lightBulb.Quotes = quotes;

			var lightBulbViewModel = new LightBulbViewModel(lightBulb);
			Assert.Equal(lightBulb.Name, lightBulbViewModel.Name);
			Assert.Equal(lightBulb.Messages.Count, lightBulbViewModel.Messages.Count);
			Assert.Equal(lightBulb.Contacts.Count, lightBulbViewModel.Contacts.Count);
			Assert.Equal(lightBulb.Quotes.Count, lightBulbViewModel.Quotes.Count);
		}

		[Fact]
		public void TestNameProperty()
		{
			var lightBulbViewModel = new LightBulbViewModel();
			var result = false;
			lightBulbViewModel.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == "Name")
				{
					result = true;
				}
			};

			lightBulbViewModel.Name = "some new name";
			Assert.True(result, "PropertyChanged was not raised for \"Name\"");
		}

		[Fact]
		public void TestMessagesProperty()
		{
			var lightBulbViewModel = new LightBulbViewModel();
			var result = false;
			lightBulbViewModel.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == "Messages")
				{
					result = true;
				}
			};

			lightBulbViewModel.Messages = new ObservableCollection<MessageViewModel>();
			Assert.True(result, "PropertyChanged was not raised for \"Messages\"");
		}

		[Fact]
		public void TestContactsProperty()
		{
			var lightBulbViewModel = new LightBulbViewModel();
			var result = false;
			lightBulbViewModel.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == "Contacts")
				{
					result = true;
				}
			};

			lightBulbViewModel.Contacts = new ObservableCollection<ContactViewModel>();
			Assert.True(result, "PropertyChanged was not raised for \"Contacts\"");
		}

		[Fact]
		public void TestQuotesProperty()
		{
			var lightBulbViewModel = new LightBulbViewModel();
			var result = false;
			lightBulbViewModel.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == "Quotes")
				{
					result = true;
				}
			};

			lightBulbViewModel.Quotes = new ObservableCollection<QuoteViewModel>();
			Assert.True(result, "PropertyChanged was not raised for \"Quotes\"");
		}	
	}
}
