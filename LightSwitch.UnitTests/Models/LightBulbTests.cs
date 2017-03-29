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

		[Fact]
		public void TestEqualObjectsAreEqual()
		{
			var id = 777;
			var name = "test name";
			var messages = new List<Message>();
			messages.Add(new Message
			{
				ID = 888,
				Text = "message text"
			});
			messages.Add(new Message
			{
				ID = 555,
				Text = "message 2 text"
			});

			var quotes = new List<Quote>();
			quotes.Add(new Quote
			{
				ID = 999,
				Text = "test quote 1",
				Reference = "reference 1"
			});
			quotes.Add(new Quote
			{
				ID = 333,
				Text = "test quote 2",
				Reference = "reference 2"
			});

			var contacts = new List<Contact>();
			contacts.Add(new Contact
			{
				ID = 111,
				Name = "name 1"
			});
			contacts.Add(new Contact
			{
				ID = 222,
				Name = "name 2"
			});

			var leftLightBulb = new LightBulb
			{
				ID = id,
				Name = name
			};
			leftLightBulb.Messages.AddRange(messages);
			leftLightBulb.Quotes.AddRange(quotes);
			leftLightBulb.Contacts.AddRange(contacts);

			messages.Reverse();
			quotes.Reverse();
			contacts.Reverse();

			var rightLightBulb = new LightBulb
			{
				ID = id,
				Name = name
			};
			rightLightBulb.Messages.AddRange(messages);
			rightLightBulb.Quotes.AddRange(quotes);
			rightLightBulb.Contacts.AddRange(contacts);

			Assert.Equal(leftLightBulb, rightLightBulb);
		}
	}
}
