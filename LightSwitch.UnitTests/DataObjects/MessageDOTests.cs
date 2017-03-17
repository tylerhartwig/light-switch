using System;
using NUnit.Framework;

namespace LightSwitch.UnitTests
{
	[TestFixture]
	public class MessageDOTests
	{
		private MessageDO testObject;

		[SetUp]
		public void CreateTestObject()
		{
			testObject = new MessageDO();
		}

		[Test]
		public void TestDefaultConstructor()
		{
			var messageDO = new MessageDO();
			Assert.NotNull(messageDO, "Default Constructor failed to create an object");
		}

		[Test]
		public void TestMessageConstructor()
		{
			var message = new Message
			{
				ID = 777,
				Text = "test text"
			};

			var messageDO = new MessageDO(message);
			Assert.AreEqual(message.ID, messageDO.ID, "Message Constructor failed to duplicate ID");
			Assert.AreEqual(message.Text, messageDO.Text, "Message Constructor failde to duplicate Text");
		}

		[Test]
		public void TestProperties()
		{
			var id = 777;
			testObject.ID = id;
			Assert.AreEqual(id, testObject.ID, "Set ID does not match Get ID");

			var text = "Test text";
			testObject.Text = text;
			Assert.AreEqual(text, testObject.Text, "Set Text does not match Get Text");
		}
	}
}
