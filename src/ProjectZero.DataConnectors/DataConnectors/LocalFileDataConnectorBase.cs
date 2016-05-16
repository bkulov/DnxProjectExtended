using Infragistics.Discoverables;
using Infragistics.ReportPlus.DashboardModel;
using Infragistics.ReportPlus.DataLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectZero.DataConnectors
{
    public abstract class LocalFileDataConnectorBase : DataConnectorBase
	{
		#region Constants

		protected const string LocalFileProviderName = Infragistics.ReportPlus.Providers.LocalFile.ProviderConstants.ProviderType;
		protected const string LocalFileProviderRootFolder = Infragistics.ReportPlus.Providers.LocalFile.ProviderSettings.RootFolder;

		protected const string DataSourceId = "__LOCAL";
		protected const int MaxRowsLimit = 100;

		#endregion Constants

		#region Fields

		protected readonly IEditorSupportService _metadataLayerService;
		protected readonly IDataProviderServices _dataProviderServices;
		protected readonly IDataProvider _dataProvider;

		#endregion Fields

		#region Constructors
		public LocalFileDataConnectorBase() : base()
		{
			try
			{
				this._metadataLayerService = Discoverables.Engine.Resolve<IEditorSupportService>();
				this._dataProviderServices = Discoverables.Engine.Resolve<IDataProviderServices>();
				this._dataProvider = this.InitDataProviderField();
			}
			catch (Exception ex)
			{
				// TODO: log exception
			}
		}
		#endregion Constructors

		#region Methods

		protected abstract IDataProvider InitDataProviderField();

		protected IMetadataItem GetMetadataItemFromFile(string filePath)
		{
			string fileDirectory = Path.GetDirectoryName(filePath);
			string fileName = Path.GetFileName(filePath);

			// replace the original root folder with the one where the desired file is located
			ISettingsProvider settings = this._dataProviderServices.GetSettingsProvider(LocalFileProviderName);
			string originalFolder = settings.GetSetting(LocalFileProviderRootFolder, string.Empty);
			settings.SetSetting(LocalFileProviderRootFolder, fileDirectory);

			// get the metadata of the desired file
			IDataSourceMetadata localRoot = this._metadataLayerService.GetDataSourceMetadata(new ServerLocation(new DataSource { Id = DataSourceId, Provider = LocalFileProviderName }));
			IList<IMetadataItem> children = this._metadataLayerService.GetContentMetadata(localRoot);
			IMetadataItem item = children.FirstOrDefault(c => c.DisplayName == fileName);

			// restore the original root folder
			settings.SetSetting(LocalFileProviderRootFolder, originalFolder);

			return item;
		}

		protected string LoadDataAsJson(IMetadataItem item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}

			// prepare the requirements of the data provider
			IDataLocation dataLocation = ((IDataSourceItemMetadata)item).DataLocation;
			IList<ISchemaColumn> schema = this._metadataLayerService.GetSchema(dataLocation);
			var dataRequest = new ConnectorsDataRequest(dataLocation)
			{
				RequestSettings = new DataRequestSettings { CacheMode = CacheUsageMode.DefaultMode }
			};

			var builder = new ConnectorsTableBuilder();
			var requirements = new ConnectorsDataRequirements()
			{
				DataProvider = this._dataProvider,
				DataRequest = dataRequest,
				Retrieve = new ConnectorsRetrieve(dataLocation, schema, MaxRowsLimit)
			};

			// load data in table
			this._dataProvider.LoadData(requirements, builder);

			// convert table to json string
			string result = builder.ResultAsJson(schema);

			return result;
		}

		#endregion Methods
	}
}
