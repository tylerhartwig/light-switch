using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

				var propertyType = xValue.GetType();
				var propertyTypeInfo = propertyType.GetTypeInfo();

				if (propertyTypeInfo.ImplementedInterfaces.Contains(typeof(IEnumerable)) && propertyTypeInfo != typeof(String).GetTypeInfo())
				{
					var typeParameters = propertyType.GenericTypeArguments;

					var seqEqualMethod = typeof(Enumerable).GetTypeInfo().DeclaredMethods.Where(m => m.Name == "SequenceEqual" && m.GetParameters().Count() == 2).First();
					var realMethod = seqEqualMethod.MakeGenericMethod(new Type[] { typeParameters[0] });

					if (!(bool)realMethod.Invoke(null, new object[] { xValue, yValue }))
					{
						return false;
					}
				}
				else
				{
					if (!xValue.Equals(yValue))
					{
						return false;
					}
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
