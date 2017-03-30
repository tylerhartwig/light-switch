using System;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class MessageViewModelTests
	{
		[Fact]
		public void TestDefaultConstructor()
		{
			var messageViewModel = new MessageViewModel();
			Assert.NotNull(messageViewModel);
		}

		[Fact]
		public void TestMessageConstructor()
		{
			var message = new Message
			{
				ID = 777,
				Text = "This is my message text"
			};

			var messageViewModel = new MessageViewModel(message);
			Assert.Equal(message.Text, messageViewModel.Text);
		}

		[Fact]
		public void TestTextProperty()
		{
			var messageViewModel = new MessageViewModel();
			var result = false;
			messageViewModel.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == "Text")
				{
					result = true;
				}
			};

			messageViewModel.Text = "new text";

			Assert.True(result, "PropertyChanged was not raised for \"Text\"");
		}
	}
}
