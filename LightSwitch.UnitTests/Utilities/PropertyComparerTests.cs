using System;
using Xunit;

namespace LightSwitch.UnitTests
{
	public class PropertyComparerTests
	{
		[Fact]
		public void TestDefaultConstructor()
		{
			var comparer = new PropertyComparer<LightBulb>();
			Assert.NotNull(comparer);
		}

		[Fact]
		public void TestEqualObjectsAreEqual()
		{
			var lightBulb1 = new LightBulb
			{
				ID = 777,
				Name = "test name"
			};

			var lightBulb2 = new LightBulb
			{
				ID = 777,
				Name = "test name"
			};

			var comparer = new PropertyComparer<LightBulb>();
			Assert.True(comparer.Equals(lightBulb1, lightBulb2), "Equal properties on object returned false");
		}

		[Fact]
		public void TestUnEqualObjectsAreUnEqual()
		{
			var lightBulb1 = new LightBulb
			{
				ID = 778,
				Name = "test name"
			};

			var lightBulb2 = new LightBulb
			{
				ID = 777,
				Name = "test name"
			};

			var comparer = new PropertyComparer<LightBulb>();
			Assert.False(comparer.Equals(lightBulb1, lightBulb2), "UnEqual properties on object returned true");
		}
	}
}
