using System;
using NUnit.Framework;

namespace LightSwitch.UnitTests
{
	[TestFixture]
	public class QuoteTests
	{
		[Test]
		public void TestDefaultConstructor()
		{
			var quote = new Quote();
			Assert.NotNull(quote);
		}

		[Test]
		public void TestGetSetText()
		{
			var text = "test text";
			var quote = new Quote();
			quote.Text = text;
			Assert.AreEqual(text, quote.Text);
		}

		[Test]
		public void TestGetSetReference()
		{
			var reference = "test reference";
			var quote = new Quote();
			quote.Reference = reference;
			Assert.AreEqual(reference, quote.Reference);
		}
	}
}
