using Infragistics.ReportPlus.DataLayer;
using System.Collections.Generic;

namespace ProjectZero.DataConnectors
{
	public class ConnectorsTableBuilder : ITableBuilder
	{
		#region Properties

		public bool IsDataTruncated { get; private set; }
		public int? MaxRowsLimit { get; set; }
		public IList<ITableColumn> InputColumns { get; }
		public IList<IList<object>> ResultRows { get; }
		public void AddColumn(ITableColumn column) => InputColumns.Add(column);
		public bool AddRow(IList<object> row)
		{
			if (this.MaxRowsLimit != null && ResultRows.Count >= this.MaxRowsLimit)
			{
				this.IsDataTruncated = true;
			}
			else
			{
				ResultRows.Add(row);
			}

			return !this.IsDataTruncated;
		}

		#endregion Properties

		#region Constructors

		public ConnectorsTableBuilder(int? maxRowsLimit = null)
		{
			this.MaxRowsLimit = maxRowsLimit;
			this.InputColumns = new List<ITableColumn>();
			this.ResultRows = new List<IList<object>>();
		}

		#endregion Constructors
	}
}
