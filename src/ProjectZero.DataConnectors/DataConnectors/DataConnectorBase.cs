namespace ProjectZero.DataConnectors
{
	public abstract class DataConnectorBase
	{
		public DataConnectorBase()
		{
			Initializer.InitDiscoverables();
		}
	}
}
