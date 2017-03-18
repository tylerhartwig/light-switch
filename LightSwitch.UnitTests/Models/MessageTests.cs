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
