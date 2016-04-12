using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectZero.Core;

namespace WebApplication.Services
{
	public class ZeroProjectsService
	{
		private readonly Dictionary<string, ZeroProject> _projectsMap = new Dictionary<string, ZeroProject>();

		internal ZeroProject GetProject(string path)
		{
			if (this._projectsMap.ContainsKey(path))
				return this._projectsMap[path];

			return null;
		}

		internal ZeroProject AddProject(string path)
		{
			ZeroProject project;
			if (this._projectsMap.ContainsKey(path))
			{
				project = this._projectsMap[path];
			}
			else
			{
				project = new ZeroProject();
				bool result = project.Load(path);
				if (result)
				{
					this._projectsMap[path] = project;
				}
				else
					return null;
			}

			return project;
		}

		internal bool SaveProject(string path)
		{
			if (this._projectsMap.ContainsKey(path))
			{
				ZeroProject project = this._projectsMap[path];
				project.Save();

				return true;
			}

			return false;
		}

		internal ZeroView AddViewToProject(string projectPath, string viewTitle)
		{
			if (this._projectsMap.ContainsKey(projectPath))
			{
				ZeroProject project = this._projectsMap[projectPath];
				ZeroView view = project.AddView(viewTitle);

				return view;
			}

			return null;
		}
	}
}
