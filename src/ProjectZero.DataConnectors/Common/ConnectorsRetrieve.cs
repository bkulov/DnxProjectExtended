using Infragistics.ReportPlus.DataLayer;
using System.Collections.Generic;
using System.Linq;

namespace ProjectZero.DataConnectors
{
	internal class ConnectorsRetrieve : ITabularRetrieveTransformation
	{
		#region Properties

		public IList<IDataComputation> Compute { get; }
		public IList<IDataFormatter> FormatAs { get; }
		public IList<IDataFilter> FilterBy { get; }
		public IList<IDataSorting> OrderBy { get; }
		public IList<ITableColumn> Hide { get; }
		public IDataLocation DataLocation { get; }
		public int? MaxRowsLimit { get; }
		public IList<ISchemaColumn> OriginalSchema { get; }
		public IList<ITableColumn> SourceSchema { get; }
		public IList<ITableColumn> RequiredColumns { get; }

		#endregion Properties

		#region Constructors

		public ConnectorsRetrieve(IDataLocation dataLocation, IList<ISchemaColumn> schema, int? maxRowsLimit)
		{
			this.Compute = new List<IDataComputation>();
			this.FormatAs = new List<IDataFormatter>();
			this.FilterBy = new List<IDataFilter>();
			this.Hide = new List<ITableColumn>();
			this.OrderBy = new List<IDataSorting>();
			this.DataLocation = dataLocation;
			this.OriginalSchema = schema;
			this.SourceSchema = schema.Cast<ITableColumn>().ToList();
			this.RequiredColumns = schema.Cast<ITableColumn>().ToList();
			this.MaxRowsLimit = maxRowsLimit;
		}

		#endregion Constructors
	}
}
