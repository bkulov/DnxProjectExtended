using Infragistics.Discoverables;
using Infragistics.ReportPlus.SecurityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectZero.DataConnectors
{
	internal class ConnectorsSecurityLayer : ISecurityLayerService
	{
		#region Fields
		private readonly IDictionary<string, IAuthenticationInfo> store = new Dictionary<string, IAuthenticationInfo>();
		#endregion Fields

		#region Properties

		private IServiceResolver ServiceResolver { get; }
		public IAuthenticationInfo GetAuthenticationInfo(IProtectedResource resource) => store.ContainsKey(resource.SecurityId) ? store[resource.SecurityId] : null;
		public void SaveAuthenticationInfo(IProtectedResource resource, IAuthenticationInfo authenticationInfo) => store[resource.SecurityId] = authenticationInfo;
		public void RemoveAuthenticationInfo(IProtectedResource resource) => store.Remove(resource.SecurityId);

		#endregion Properties

		#region Constructors

		public ConnectorsSecurityLayer(IServiceResolver resolver)
		{
			ServiceResolver = resolver;
		}

		#endregion Constructors
	}

}
