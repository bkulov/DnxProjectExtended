﻿using Newtonsoft.Json;
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
		private List<View> _views;
		private List<Resource> _resources;

		#endregion Fields

		#region Properties
		public DnxProject InternalProject
		{
			get
			{
				return this._dnxProject;
			}
		}

		public IEnumerable<View> Views
		{
			get
			{
				return this._views;
			}
		}

		public IEnumerable<Resource> Resources
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
			this._resources = new List<Resource>();
			this._views = new List<View>();
        }
		#endregion Constructors

		#region Public methods

		public bool Load(string projectFolderPath)
		{
			// TODO: add validation and error checks
			try
			{
				if (DnxProject.HasProjectFile(projectFolderPath) &&
					DnxProject.TryGetProject(projectFolderPath, out this._dnxProject))
				{
					var prjContent = File.ReadAllText(this._dnxProject.ProjectFilePath);
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

		public void Save()
		{
			if (this._dnxProject == null)
				return;

			try
			{
				var serializer = new JsonSerializer();
				serializer.Formatting = Formatting.Indented;
			}
			catch (Exception ex)
			{
				// TODO: log exception
			}
		}

		public View AddView(string title)
		{
			var view = new View(title);

			this._views.Add(view);

			return view;
		}

		public void DeleteView(View view, bool deleteResources = false)
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
