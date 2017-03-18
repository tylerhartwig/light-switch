using System;
using System.Collections.Generic;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class LightBulbTests
	{
		[Fact]
		public void TestDefaultConstructor()
		{
			var lightBulb = new LightBulb();
			Assert.NotNull(lightBulb);
		}

		[Fact]
		public void TestLightBulbDOConstructor()
		{
			var lightBulbDO = new LightBulbDO
			{
				ID = 777,
				Name = "Test name"
			};

			var lightBulb = new LightBulb(lightBulbDO);
			Assert.NotNull(lightBulb);
			Assert.Equal(lightBulbDO.ID, lightBulb.ID);
			Assert.Equal(lightBulbDO.Name, lightBulb.Name);
		}

		[Fact]
		public void TestProperties()
		{
			var testObject = new LightBulb();

			var id = 777;
			testObject.ID = id;
			Assert.Equal(id, testObject.ID);

			var name = "test name";
			testObject.Name = name;
			Assert.Equal(name, testObject.Name);

			var messages = new List<Message>();
			messages.Add(new Message
			{
				ID = 777,
				Text = "test text"
			});
			testObject.Messages = messages;
			Assert.Equal(messages, testObject.Messages);

			var contacts = new List<Contact>();
			contacts.Add(new Contact
			{
				ID = 777,
				Name = "test name"
			});
			testObject.Contacts = contacts;
			Assert.Equal(contacts, testObject.Contacts);

			var quotes = new List<Quote>();
			quotes.Add(new Quote
			{
				ID = 777,
				Text = "Test quote",
				Reference = "test reference"
			});
			testObject.Quotes = quotes;
			Assert.Equal(quotes, testObject.Quotes);
		}
	}
}
