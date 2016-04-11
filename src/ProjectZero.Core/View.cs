using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectZero.Core
{
	public class View
    {
		#region Fields

		private Guid _id; // GUID
		private List<Resource> _resources;

		#endregion Fields

		#region Properties

		public Guid Id
		{
			get
			{
				return this._id;
			}
		}

		public string Title { get; set; }

		public IEnumerable<Resource> Resources
		{
			get
			{
				return this._resources;
			}
		}

		#endregion Properties

		#region Constructors

		public View(string title)
		{
			this._id = new Guid();
			this.Title = title;
			this._resources = new List<Resource>();
		}

		#endregion Constructors

		#region Public Methods

		public void AddResource(Resource resource)
		{
			if (this._resources.Contains(resource))
				return;

			this._resources.Add(resource);
			resource.IncreaseReferenceCounter();
		}

		public void DeleteResource(Resource resource)
		{
			if (!this._resources.Contains(resource))
				return;

			this._resources.Remove(resource);
			resource.DecreaseReferenceCounter();
		}

		public void CleanupResources()
		{
			foreach (var resource in this._resources)
			{
				this.DeleteResource(resource);
			}
		}

		#endregion Public Methods
	}
}
