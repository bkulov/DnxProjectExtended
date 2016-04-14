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

		#endregion Properties

		#region Constructors
		public ZeroProject()
        {
			this._views = new List<ZeroView>();
        }
		#endregion Constructors

		#region Public methods

		public static bool TryLoadProject(string projectFolderPath, out ZeroProject project)
		{
			try
			{
				project = new ZeroProject();
				if (DnxProject.TryGetProject(projectFolderPath, out project._dnxProject))
				{
					string prjContent = File.ReadAllText(project._dnxProject.ProjectFilePath);

					JsonConvert.PopulateObject(prjContent, project);

					return true;
				}
			}
			catch (Exception ex)
			{
				// TODO: log exception
			}

			project = null;
			return false;
		}

		public void Save(string filePath = null)
		{
			if (this._dnxProject == null)
				return;

			try
			{
				var serializer = new JsonSerializer();

				// set some serializator settings
				serializer.Formatting = Formatting.Indented;
				serializer.DefaultValueHandling = DefaultValueHandling.Ignore;
				serializer.NullValueHandling = NullValueHandling.Ignore;
				serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();

				// serialize both the internal (DNX) project and the additional elements from this object
				// then merge both JTokens into one to be saved to file
				JToken prjToken = JToken.FromObject(this.InternalProject, serializer);
				JToken thisToken = JToken.FromObject(this, serializer);
				((JContainer)prjToken).Merge(thisToken);

				// save merged object to file
				using (var writer = File.CreateText((filePath ?? this._dnxProject.ProjectFilePath)))
				{
					serializer.Serialize(writer, prjToken);
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
		}

		// TODO: ???
		public void Package() { }
		public void EditView() { }
		#endregion Public methods
	}
}
