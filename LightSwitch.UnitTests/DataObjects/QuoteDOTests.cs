using System;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class QuoteDOTests
	{
		[Fact]
		public void TestDefaultConstructor()
		{
			var quoteDO = new QuoteDO();
			Assert.NotNull(quoteDO);
		}

		[Fact]
		public void TestQuoteConstructor()
		{
			var quote = new Quote
			{
				ID = 777,
				Text = "Test Quote Text",
				Reference = "Test Reference"
			};

			var quoteDO = new QuoteDO(quote);
			Assert.NotNull(quoteDO);
			Assert.Equal(quote.ID, quoteDO.ID);
			Assert.Equal(quote.Text, quoteDO.Text);
			Assert.Equal(quote.Reference, quoteDO.Reference);
		}

		[Fact]
		public void TestProperties()
		{
			var testObject = new QuoteDO();

			var id = 777;
			testObject.ID = id;
			Assert.Equal(id, testObject.ID);

			var text = "test text";
			testObject.Text = text;
			Assert.Equal(text, testObject.Text);

			var reference = "test reference";
			testObject.Reference = reference;
			Assert.Equal(reference, testObject.Reference);
		}
	}
}
