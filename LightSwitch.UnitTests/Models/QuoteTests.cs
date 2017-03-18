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
	}
}
