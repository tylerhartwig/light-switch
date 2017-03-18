using System;
using System.Collections.Generic;
using System.Reflection;

namespace LightSwitch
{
	public class PropertyComparer<T> : IEqualityComparer<T>
	{
		public bool Equals(T x, T y)
		{
			Type type = x.GetType();
			var typeInfo = type.GetTypeInfo();

			var properties = typeInfo.DeclaredProperties;
			foreach (var property in properties)
			{
				var xValue = property.GetValue(x);
				var yValue = property.GetValue(y);
				if (!xValue.Equals(yValue))
				{
					return false;
				}
			}

			return true;
		}

		public int GetHashCode(T obj)
		{
			throw new NotImplementedException();
		}
	}
}
