using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DnxProject = Microsoft.Dnx.Runtime.Project;

namespace ProjectZero.Core
{
	public class ZeroProject
    {
		#region Fields
		private DnxProject _dnxProject;
		private List<ZeroView> _views;
		private List<ZeroResource> _resources;

		#endregion Fields

		#region Properties

		[JsonIgnore]
		public DnxProject InternalProject
		{
			get
			{
				return this._dnxProject;
			}
		}

		public IEnumerable<ZeroView> Views
		{
			get
			{
				return this._views;
			}
		}


		// TODO: we don't need this because its present in DNX project - sourceFiles, preprocessSourceFiles, resourceFiles, sharedFiles
		public IEnumerable<ZeroResource> Resources
		{
			get
			{
				return this._resources;
			}
		}
		#endregion Properties

		#region Constructors
		public ZeroProject()
        {
			this._resources = new List<ZeroResource>();
			this._views = new List<ZeroView>();
        }
		#endregion Constructors

		#region Public methods


		// TODO: Use a static factory method instead.
		public bool Load(string projectFolderPath)
		{
			// TODO: add validation and error checks
			try
			{
				if (DnxProject.TryGetProject(projectFolderPath, out this._dnxProject))
				{
					string prjContent = File.ReadAllText(this._dnxProject.ProjectFilePath);
					//var prj1 = new DnxProject();
					//JsonConvert.PopulateObject(prjContent, prj1);		// Throws exceptions in SemanticVersion deserialization

					JsonConvert.PopulateObject(prjContent, this);

					return true;
				}
			}
			catch (Exception ex)
			{
				// TODO: log exception
			}

			return false;
		}

		public void Save(string filePath = null)
		{
			if (this._dnxProject == null)
				return;

			try
			{
				var serializer = new JsonSerializer();
				serializer.Formatting = Formatting.Indented;
				serializer.DefaultValueHandling = DefaultValueHandling.Ignore;
				serializer.NullValueHandling = NullValueHandling.Ignore;
				serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();

				// TODO: remove .json extension
				using (var writer = File.CreateText((filePath ?? this._dnxProject.ProjectFilePath) + ".json"))
				{
					serializer.Serialize(writer, this.InternalProject);
					serializer.Serialize(writer, this);
				}

				using (var wr = new JTokenWriter())
				{
					serializer.Serialize(wr, this.InternalProject);
					serializer.Serialize(wr, this.InternalProject);
					var aa = ((JObject)(wr.Token)).ToString();
				}
			}
			catch (Exception ex)
			{
				// TODO: log exception
			}
		}

		public ZeroView AddView(string title)
		{
			var view = new ZeroView(title);

			this._views.Add(view);

			return view;
		}

		public void DeleteView(ZeroView view, bool deleteResources = false)
		{
			if (!this._views.Contains(view))
				return;

			this._views.Remove(view);

			var viewResources = view.Resources.ToArray();

			// clean view's resources and remove them from the project's resources if no longer in use
			view.CleanupResources();

			if (deleteResources)
			{
				foreach (var resource in viewResources)
				{
					if (resource.ReferenceCounter == 0 && this._resources.Contains(resource))
						this._resources.Remove(resource);
				}
			}
		}

		public void ClearResources()
		{
			this._resources.Clear();

			// TODO: check if the resources should be removed from existing views too
		}

		// TODO: ???
		public void Package() { }
		public void EditView() { }
		#endregion Public methods
	}
}
