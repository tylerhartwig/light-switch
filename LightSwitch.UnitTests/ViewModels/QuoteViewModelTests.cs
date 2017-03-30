using System;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class QuoteViewModelTests
	{
		[Fact]
		public void TestDefaultConstructor()
		{
			var quoteViewModel = new QuoteViewModel();
			Assert.NotNull(quoteViewModel);
		}

		[Fact]
		public void TestQuoteConstructor()
		{
			var quote = new Quote
			{
				ID = 777,
				Text = "test test test text",
				Reference = "Some guy said this"
			};

			var quoteViewModel = new QuoteViewModel(quote);
			Assert.Equal(quote.Text, quoteViewModel.Text);
			Assert.Equal(quote.Reference, quoteViewModel.Reference);
		}

		[Fact]
		public void TestTextProperty()
		{
			var quoteViewModel = new QuoteViewModel();
			var result = false;
			quoteViewModel.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == "Text")
				{
					result = true;
				}
			};

			quoteViewModel.Text = "some test text";
			Assert.True(result, "PropertyChanged was not raised for \"Text\"");
		}

		[Fact]
		public void TestReferenceProperty()
		{
			var quoteViewModel = new QuoteViewModel();
			var result = false;
			quoteViewModel.PropertyChanged += (sender, e) =>
			{
				result = true;
			};

			quoteViewModel.Reference = "Some reference";
			Assert.True(result, "PropertyChanged was not raised for \"Reference\"");
		}
	}
}
