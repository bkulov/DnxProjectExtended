using Infragistics.ReportPlus.DataLayer;
using Newtonsoft.Json.Linq;
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

		#region Methods
		public string ResultAsJson(IList<ISchemaColumn> schema)
		{
			var rows = new JArray();

			foreach (var resultRow in this.ResultRows)
			{
				var resultRowData = new JArray(resultRow);
				var row = new JObject();

				for (int index = 0; index < schema.Count; index++)
				{
					row.Add(propertyName: schema[index].ColumnName, value: resultRowData[index]);
				}

				rows.Add(row);
			}

			return rows.ToString();
		}
		#endregion Methods
	}
}
