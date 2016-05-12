using Infragistics.ReportPlus.DataLayer;
using System.Collections.Generic;

namespace ProjectZero.DataConnectors
{
	internal class ConnectorsDataRequirements : IDataRequirements
	{
		#region Properties

		public IDataRequest DataRequest { get; set; }
		public IDataProvider DataProvider { get; set; }
		public IRetrieveTransformation Retrieve { get; set; }
		public IList<IDataTransformation> Transformations { get; }
		public IList<IDataTransformation> PostProcessing { get; }
		public IDataLocation DataLocation => DataRequest?.DataLocation;

		#endregion Properties

		#region Constructors

		public ConnectorsDataRequirements()
		{
			Transformations = new List<IDataTransformation>();
			PostProcessing = new List<IDataTransformation>();
		}
	
		#endregion Constructors
	}
}
