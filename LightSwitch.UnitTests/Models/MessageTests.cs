using System;
using NUnit.Framework;

namespace LightSwitch.UnitTests
{
	[TestFixture]
	public class MessageTests
	{
		private Message testObject;

		[SetUp]
		public void CreateTestObject()
		{
			testObject = new Message();
		}

		[Test]
		public void TestDefaultConstructor()
		{
			var message = new Message();
			Assert.NotNull(message);
		}

		[Test]
		public void TestProperties()
		{
			var id = 777;
			testObject.ID = id;
			Assert.AreEqual(id, testObject.ID, "Set ID does not match Get ID");

			var text = "test text";
			testObject.Text = text;
			Assert.AreEqual(text, testObject.Text, "Set Text does not match Get Text");
		}
	}
}
