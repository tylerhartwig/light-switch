using System;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class QuoteTests
	{
		[Fact]
		public void TestDefaultConstructor()
		{
			var quote = new Quote();
			Assert.NotNull(quote);
		}

		[Fact]
		public void TestQuoteDOConstructor()
		{
			var quoteDO = new QuoteDO
			{
				ID = 777,
				Text = "test text",
				Reference = "test reference"
			};

			var quote = new Quote(quoteDO);
			Assert.Equal(quoteDO.ID, quote.ID);
			Assert.Equal(quoteDO.Text, quote.Text);
			Assert.Equal(quoteDO.Reference, quote.Reference);
		}

		[Fact]
		public void TestProperties()
		{
			var testObject = new Quote();

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

		[Fact]
		public void TestEqualObjectsAreEqual()
		{
			var id = 777;
			var text = "test text test";
			var reference = "test reference";

			var leftQuote = new Quote
			{
				ID = id,
				Text = text,
				Reference = reference
			};

			var rightQuote = new Quote
			{
				ID = id,
				Text = text,
				Reference = reference
			};

			Assert.Equal(leftQuote, rightQuote);
		}

		[Fact]
		public void TestUnEqualObjectsAreUnEqual()
		{
			var id1 = 777;
			var id2 = 888;
			var text1 = "Test text 1";
			var text2 = "Test text 2";
			var reference1 = "refenence 1";
			var reference2 = "refenence 2";

			var leftQuote = new Quote
			{
				ID = id1
			};

			var rightQuote = new Quote
			{
				ID = id2
			};

			Assert.NotEqual(leftQuote, rightQuote);

			leftQuote.Text = text1;
			rightQuote.ID = id1;
			rightQuote.Text = text2;

			Assert.NotEqual(leftQuote, rightQuote);

			leftQuote.Reference = reference1;
			rightQuote.Text = text1;
			rightQuote.Reference = reference2;

			Assert.NotEqual(leftQuote, rightQuote);
		}
	}
}
