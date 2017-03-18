using System;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class MessageTests
	{
		[Fact]
		public void TestDefaultConstructor()
		{
			var message = new Message();
			Assert.NotNull(message);
		}

		[Fact]
		public void TestMessageDOConstructor()
		{
			var messageDO = new MessageDO
			{
				ID = 777,
				Text = "test text"
			};

			var message = new Message(messageDO);
			Assert.NotNull(message);
			Assert.Equal(messageDO.ID, message.ID);
			Assert.Equal(messageDO.Text, message.Text);
		}

		[Fact]
		public void TestProperties()
		{
			var testObject = new Message();

			var id = 777;
			testObject.ID = id;
			Assert.Equal(id, testObject.ID);

			var text = "test text";
			testObject.Text = text;
			Assert.Equal(text, testObject.Text);
		}
	}
}
