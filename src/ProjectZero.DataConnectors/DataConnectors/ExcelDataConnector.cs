using Infragistics.ReportPlus.DataLayer;
using Infragistics.ReportPlus.Providers.Excel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectZero.DataConnectors
{
	// This project can output the Class library as a NuGet Package.
	// To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
	public class ExcelDataConnector : LocalFileDataConnectorBase
	{
		public string GetData(string filePath, string[] sheetNames)
		{
			try
			{
				if (sheetNames == null || sheetNames.All(x => string.IsNullOrEmpty(x)))
				{
					return string.Empty;
				}

				IMetadataItem item = this.GetMetadataItemFromFile(filePath);

				// find the desired sheet in item's children array
				IList<IMetadataItem> itemChildren = this._metadataLayerService.GetContentMetadata(item);

				IEnumerable<IMetadataItem> sheets = itemChildren.Where(c => c.ItemType == MetadataConstants.SHEET && sheetNames.Contains(c.DisplayName, new StringIgnoreCaseComparer()));

				return this.LoadDataAsJson(sheets);
			}
			catch (Exception ex)
			{
				// TODO: log the exception
				throw ex;
			}
		}

		protected override IDataProvider InitDataProviderField()
		{
			return this._dataProviderServices.GetDataProvider(ProviderConstants.ProviderType);
		}
	}
}
