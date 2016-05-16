using Infragistics.ReportPlus.DataLayer;
using Infragistics.ReportPlus.Providers.Csv;
using System;

namespace ProjectZero.DataConnectors
{
	// This project can output the Class library as a NuGet Package.
	// To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
	public class CSVDataConnector : LocalFileDataConnectorBase
	{
		public string GetData(string filePath)
		{
			try
			{
				IMetadataItem item = this.GetMetadataItemFromFile(filePath);

				return this.LoadDataAsJson(item);
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
