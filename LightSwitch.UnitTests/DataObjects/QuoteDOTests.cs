using System;
using NUnit.Framework;

namespace LightSwitch.UnitTests
{
	[TestFixture]
	public class QuoteDOTests
	{
		private QuoteDO testObject;

		[SetUp]
		public void CreateTestObject()
		{
			testObject = new QuoteDO();
		}

		[Test]
		public void TestDefaultConstructor()
		{
			var quoteDO = new QuoteDO();
			Assert.NotNull(quoteDO);
		}

		[Test]
		public void TestQuoteConstructor()
		{
			var quote = new Quote
			{
				ID = 777,
				Text = "Test Quote Text",
				Reference = "Test Reference"
			};

			var quoteDO = new QuoteDO(quote);
			Assert.NotNull(quoteDO, "Quote Constructor failed to create an object");
			Assert.AreEqual(quote.ID, quoteDO.ID, "Quote Constructor failed to duplicate ID");
			Assert.AreEqual(quote.Text, quoteDO.Text, "Quote Constructor failed to duplicate Text");
			Assert.AreEqual(quote.Reference, quoteDO.Reference, "Quote Constructor failed to duplicate Reference");
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
