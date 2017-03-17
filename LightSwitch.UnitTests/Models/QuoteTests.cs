using System;
using NUnit.Framework;

namespace LightSwitch.UnitTests
{
	[TestFixture]
	public class QuoteTests
	{
		private Quote testObject;

		[SetUp]
		public void CreateTestObject()
		{
			testObject = new Quote();
		}

		[Test]
		public void TestDefaultConstructor()
		{
			var quote = new Quote();
			Assert.NotNull(quote, "Default constructor failed to create object");
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

			var reference = "test reference";
			testObject.Reference = reference;
			Assert.AreEqual(reference, testObject.Reference, "Set Reference does not match Get Reference");
		}
	}
}
