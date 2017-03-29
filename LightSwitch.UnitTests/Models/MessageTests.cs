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

		[Fact]
		public void TestEqualObjectsAreEqual()
		{
			var id = 777;
			var text = "Some test text";

			var leftMessage = new Message
			{
				ID = id,
				Text = text
			};

			var rightMessage = new Message
			{
				ID = id,
				Text = text
			};

			Assert.Equal(leftMessage, rightMessage);
		}

		[Fact]
		public void TestUnEqualObjectsAreUnEqual()
		{
			var id1 = 777;
			var id2 = 888;
			var text1 = "some test text";
			var text2 = "other test text";

			var leftMessage = new Message
			{
				ID = id1,
				Text = text1
			};

			var rightMessage = new Message
			{
				ID = id1,
				Text = text2
			};

			Assert.NotEqual(leftMessage, rightMessage);

			rightMessage.Text = text1;
			rightMessage.ID = id2;

			Assert.NotEqual(leftMessage, rightMessage);
		}
	}
}
