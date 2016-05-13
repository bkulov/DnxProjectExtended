using Infragistics.Discoverables;
using Infragistics.ReportPlus.DashboardModel;
using Infragistics.ReportPlus.DataLayer;
using Infragistics.ReportPlus.Providers.Csv;
using System;
using System.IO;
using System.Linq;

namespace ProjectZero.DataConnectors
{
	// This project can output the Class library as a NuGet Package.
	// To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
	public class CSVDataConnector : LocalFileDataConnectorBase
	{
		public string GetData(string filePath)
		{
			var dataProviderServices = Discoverables.Engine.Resolve<IDataProviderServices>();
			var dataProvider = dataProviderServices.GetDataProvider(ProviderConstants.ProviderType);
			var metadataLayerService = Discoverables.Engine.Resolve<IEditorSupportService>();

			string fileDirectory = Path.GetDirectoryName(filePath);
			string fileName = Path.GetFileName(filePath);

			try
			{
				// replace the original root folder with the one where the desired file is located
				var settings = dataProviderServices.GetSettingsProvider(LocalFileProviderName);
				var originalFolder = settings.GetSetting(LocalFileProviderRootFolder, string.Empty);
				settings.SetSetting(LocalFileProviderRootFolder, fileDirectory);

				// get the metadata of the desired file
				var localRoot = metadataLayerService.GetDataSourceMetadata(new ServerLocation(new DataSource { Id = "__LOCAL", Provider = LocalFileProviderName }));
				var children = metadataLayerService.GetContentMetadata(localRoot);
				var item = children.FirstOrDefault(c => c.DisplayName == fileName);

				// restore the original root folder
				settings.SetSetting(LocalFileProviderRootFolder, originalFolder);

				// prepare the requirements of the data provider
				var dataLocation = ((IDataSourceItemMetadata)item).DataLocation;
				var schema = metadataLayerService.GetSchema(dataLocation);
				var dataRequest = new ConnectorsDataRequest(dataLocation);
				dataRequest.RequestSettings = new DataRequestSettings { CacheMode = CacheUsageMode.DefaultMode };

				var requirements = new ConnectorsDataRequirements() { DataProvider = dataProvider, DataRequest = dataRequest, Retrieve = new ConnectorsRetrieve(dataLocation, schema, MaxRowsLimit) };
				var builder = new ConnectorsTableBuilder();

				// load data in array of objects
				dataProvider.LoadData(requirements, builder);

				// TODO: bvk - convert the result to json

				var result = builder.ResultAsJson(schema);
				return result;
			}
			catch (Exception ex)
			{
				// TODO: log the exception
				throw ex;
			}
		}
	}
}
