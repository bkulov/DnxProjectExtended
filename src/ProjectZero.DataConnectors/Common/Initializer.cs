using Infragistics.Discoverables;
using Infragistics.ReportPlus.SecurityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectZero.DataConnectors
{
    public static class Initializer
    {
		private static bool _isInitialized = false;

		public static void InitDiscoverables()
		{
			if (!_isInitialized)
			{
				// TODO: bvk - load dlls from correct place

				Discoverables.Initialize(new EngineSetup()
					.Service<ISecurityLayerService>(resolver => new ConnectorsSecurityLayer(resolver))
					.SharedFolder(@"C:\Work\Samples\Web\WebApplication\src\ProjectZero.DataConnectors\Binaries\Shared")
					.SharedFolder(@"C:\Work\Samples\Web\WebApplication\src\ProjectZero.DataConnectors\Binaries\APIs")
					//.SharedFolder(@"C:\Work\Samples\Web\WebApplication\src\ProjectZero.DataConnectors\Binaries\IMPs")
					//.SharedFolder(@"C:\Work\Samples\Web\WebApplication\src\ProjectZero.DataConnectors\Binaries\Providers")
					//.ScanFolder(@"C:\Work\Samples\Web\WebApplication\src\ProjectZero.DataConnectors\Binaries\Shared")
					//.ScanFolder(@"C:\Work\Samples\Web\WebApplication\src\ProjectZero.DataConnectors\Binaries\APIs")
					.ScanFolder(@"C:\Work\Samples\Web\WebApplication\src\ProjectZero.DataConnectors\Binaries\Providers")
					.ScanFolder(@"C:\Work\Samples\Web\WebApplication\src\ProjectZero.DataConnectors\Binaries\IMPs"));

				_isInitialized = true;
			}
		}
	}
}
