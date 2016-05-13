using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectZero.DataConnectors
{
    public class LocalFileDataConnectorBase : DataConnectorBase
	{
		protected const string LocalFileProviderName = Infragistics.ReportPlus.Providers.LocalFile.ProviderConstants.ProviderType;
		protected const string LocalFileProviderRootFolder = Infragistics.ReportPlus.Providers.LocalFile.ProviderSettings.RootFolder;

		protected const int MaxRowsLimit = 100;
	}
}
