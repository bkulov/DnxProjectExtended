using Infragistics.ReportPlus.DataLayer;

namespace ProjectZero.DataConnectors
{
	internal class ConnectorsDataRequest : DataRequest
	{
		#region Fields

		private readonly IDataLocation _dataLocation;

		#endregion Fields

		#region Properties

		public override IDataLocation DataLocation
		{
			get
			{
				return this._dataLocation;
			}
		}

		#endregion Properties

		
		#region Constructors

		public ConnectorsDataRequest(IDataLocation dataLocation)
		{
			this._dataLocation = dataLocation;

			// Workaround: Recreate the request settings as the CacheMode property is null by default and doesn't have a setter.
			// CacheMode is used by DataProviders and we need to provide a default value.
			this.RequestSettings = new DataRequestSettings { CacheMode = CacheUsageMode.DefaultMode };
		}

		#endregion Constructors
	}
}
