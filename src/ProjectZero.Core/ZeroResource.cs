using System.Collections.Generic;

namespace ProjectZero.Core
{
	public class ZeroResource
	{
		#region Fields
		private int _referenceCounter = 0;
		#endregion Fields

		#region Properties
		public string Name { get; private set; }        // TODO: Do we need the name? Isn't the path sufficient?
		public string Path { get; private set; }
		public ResourceType Type { get; private set; }
		public int ReferenceCounter
		{
			get
			{
				return this._referenceCounter;
			}
		}
		#endregion Properties

		#region Constructors
		public ZeroResource(string name, string path, ResourceType type)
		{
			this.Name = name;
			this.Path = path;
			this.Type = type;
		}
		#endregion Constructors

		#region Methods
		public void IncreaseReferenceCounter()
		{
			this._referenceCounter++;
		}

		public void DecreaseReferenceCounter()
		{
			if (this._referenceCounter > 0)
				this._referenceCounter--;
		}
		#endregion Methods
	}
}