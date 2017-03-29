using System;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class MessageDOTests
	{
		[Fact]
		public void TestDefaultConstructor()
		{
			var messageDO = new MessageDO();
			Assert.NotNull(messageDO);
		}

		[Fact]
		public void TestMessageConstructor()
		{
			var message = new Message
			{
				ID = 777,
				Text = "test text"
			};

			var messageDO = new MessageDO(message);
			Assert.Equal(message.ID, messageDO.ID);
			Assert.Equal(message.Text, messageDO.Text);
		}

		[Fact]
		public void TestProperties()
		{
			var testObject = new MessageDO();

			var id = 777;
			testObject.ID = id;
			Assert.Equal(id, testObject.ID);

			var text = "Test text";
			testObject.Text = text;
			Assert.Equal(text, testObject.Text);
		}
	}
}
