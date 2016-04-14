using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectZero.Core
{
	public class ZeroView
    {
		// TODO: add some additional metadata if required

		#region Fields

		private Guid _id;
		private List<ZeroResource> _resources;

		#endregion Fields

		#region Properties

		// TODO: make the setter private. Currently the deserialization on project.json needs this to be public
		public Guid Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		public string Title { get; set; }

		public IEnumerable<ZeroResource> Resources
		{
			get
			{
				return this._resources;
			}
		}

		#endregion Properties

		#region Constructors

		public ZeroView()
		{
			this._id = Guid.NewGuid();
			this._resources = new List<ZeroResource>();
		}

		public ZeroView(string title) : this()
		{
			this.Title = title;
		}

		#endregion Constructors

		#region Public Methods

		public void AddResource(ZeroResource resource)
		{
			if (this._resources.Contains(resource))
				return;

			this._resources.Add(resource);
		}

		public void DeleteResource(ZeroResource resource)
		{
			if (!this._resources.Contains(resource))
				return;

			this._resources.Remove(resource);
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
