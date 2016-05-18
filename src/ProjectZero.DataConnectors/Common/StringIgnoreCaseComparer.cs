using System;
using System.Collections.Generic;

namespace ProjectZero.DataConnectors
{
	internal class StringIgnoreCaseComparer : IEqualityComparer<string>
	{
		public bool Equals(string x, string y)
		{
			return x.Equals(y, StringComparison.InvariantCultureIgnoreCase);
		}

		public int GetHashCode(string obj)
		{
			return obj.GetHashCode();
		}
	}
}