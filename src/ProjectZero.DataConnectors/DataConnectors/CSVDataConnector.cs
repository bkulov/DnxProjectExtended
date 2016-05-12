using System.Linq;

using Infragistics.Discoverables;
using Infragistics.ReportPlus.DataLayer;
using Infragistics.ReportPlus.Providers.Csv;
using System.IO;
using Infragistics.ReportPlus.DashboardModel;

namespace ProjectZero.DataConnectors
{
	// This project can output the Class library as a NuGet Package.
	// To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
	public class CSVDataConnector
    {
		// TODO: bvk - replace those with usings
		public const string LocalFileProviderName = "LOCALFILE"; //Infragistics.ReportPlus.Providers.LocalFile.ProviderConstants.ProviderType
		public const string LocalFileProviderRootFolder = "RootFolder"; //Infragistics.ReportPlus.Providers.LocalFile.ProviderSettings.RootFolder

		private const int MaxRowsLimit = 100;

		public void GetData(string filePath)
		{
			var metaDataProviderServices = Discoverables.Engine.Resolve<IMetaDataProviderServices>();
			var dataProviderServices = Discoverables.Engine.Resolve<IDataProviderServices>();
			var metadataProvider = metaDataProviderServices.GetMetadataProvider(ProviderConstants.ProviderType);
			var dataProvider = dataProviderServices.GetDataProvider(ProviderConstants.ProviderType);
			var metadataLayerService = Discoverables.Engine.Resolve<IEditorSupportService>();

			// TODO: bvk - remove this
			filePath = @"D:\Temp\FL_insurance_sample\FL_insurance_sample.csv";
			string fileDirectory = Path.GetDirectoryName(filePath);
			string fileName = Path.GetFileName(filePath);

			ISettingsProvider settings = metaDataProviderServices.GetSettingsProvider(LocalFileProviderName);
			string originalFolder = settings.GetSetting(LocalFileProviderRootFolder, string.Empty);
			settings.SetSetting(LocalFileProviderRootFolder, fileDirectory);

			var localRoot = metadataLayerService.GetDataSourceMetadata(new ServerLocation(new DataSource { Id = "__LOCAL", Provider = LocalFileProviderName }));
			var children = metadataLayerService.GetContentMetadata(localRoot);
			var item = children.FirstOrDefault(c => c.DisplayName == fileName);

			settings.SetSetting(LocalFileProviderRootFolder, originalFolder);

			var dataLocation = ((IDataSourceItemMetadata)item).DataLocation;
			var schema = metadataLayerService.GetSchema(dataLocation);

			var dataRequest = new ConnectorsDataRequest(dataLocation);
			dataRequest.RequestSettings = new DataRequestSettings { CacheMode = CacheUsageMode.DefaultMode };

			var requirements = new ConnectorsDataRequirements() { DataProvider = dataProvider, DataRequest = dataRequest, Retrieve = new ConnectorsRetrieve(dataLocation, schema, MaxRowsLimit) };
			var builder = new ConnectorsTableBuilder();

			dataProvider.LoadData(requirements, builder);
		}
    }
}
