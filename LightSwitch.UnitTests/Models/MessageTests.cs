using System;
using NUnit.Framework;

namespace LightSwitch.UnitTests
{
	[TestFixture]
	public class MessageTests
	{
		[Test]
		public void TestDefaultConstructor()
		{
			var message = new Message();
			Assert.NotNull(message);
		}

		[Test]
		public void TestMessageGetSetText()
		{
			var text = "test text";
			var message = new Message();
			message.Text = text;
			Assert.AreEqual(text, message.Text);
		}
	}
}
