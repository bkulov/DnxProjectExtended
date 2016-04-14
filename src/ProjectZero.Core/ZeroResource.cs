using System.Collections.Generic;

namespace ProjectZero.Core
{
	public class ZeroResource
	{
		#region Properties

		public string Path { get; private set; }

		public ResourceType Type { get; private set; }

		#endregion Properties

		#region Constructors
		public ZeroResource(string path, ResourceType type)
		{
			this.Path = path;
			this.Type = type;
		}
		#endregion Constructors
	}
}