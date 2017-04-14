using System;
using System.ComponentModel;
using System.Reflection;
using Xunit;

namespace LightSwitch.UnitTests
{
	public static class TestHelper
	{
		public static void TestPropertyChanged(object obj, string propertyName, object propertyValue)
		{
			var typeInfo = obj.GetType().GetTypeInfo();
			var property = typeInfo.GetDeclaredProperty(propertyName);

			var propertyChanged = false;

			Assert.True(obj is INotifyPropertyChanged, "Object does not implement INotifyPropertyChanged");
			var objPropertyChanged = (INotifyPropertyChanged)obj;

			objPropertyChanged.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == propertyName)
				{
					propertyChanged = true;
				}
			};

			property.SetMethod.Invoke(objPropertyChanged, new object[] { propertyValue });
			Assert.True(propertyChanged, string.Format("PropertyChanged was not raised for property \"{0}\"", propertyName));
		}
	}
}
