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
		public string GetData(string filePath, string sheetName)
		{
			try
			{
				bool isSheetNameEmpty = string.IsNullOrEmpty(sheetName);

				IMetadataItem item = this.GetMetadataItemFromFile(filePath);

				// find the desired sheet in item's children array or get the first one if sheet name is empty
				IList<IMetadataItem> itemChildren = this._metadataLayerService.GetContentMetadata(item);
				IMetadataItem sheet = itemChildren.FirstOrDefault(c => c.ItemType == MetadataConstants.SHEET && (isSheetNameEmpty || c.DisplayName == sheetName));

				return this.LoadDataAsJson(sheet);
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
