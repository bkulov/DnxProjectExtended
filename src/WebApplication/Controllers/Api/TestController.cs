using Microsoft.AspNet.Mvc;
using ProjectZero.Core;
using ProjectZero.DataConnectors;
using WebApplication.Services;

namespace WebApplication.Controllers.Api
{
	[Route("api/test")]
    public class TestController : Controller
    {
		private ZeroProjectsService _zeroProjectsService;

		public TestController(ZeroProjectsService zeroProjectsService)
		{
			this._zeroProjectsService = zeroProjectsService;
		}

		[HttpGet]
		public JsonResult Get(string path)
		{
			//var path = @"C:\Work\Samples\Web\WebApplication\src\TestClassLibrary";

			ZeroProject project = this._zeroProjectsService.GetProject(path);
			if (project == null)
			{
				project = this._zeroProjectsService.AddProject(path);
			}

			return Json(project);
		}

		[HttpPost("save")]
		public JsonResult Save(string path)
		{
			EnsureProjectLoaded(path);

			bool result = this._zeroProjectsService.SaveProject(path);
			return Json(result);
		}

		[HttpPost("addview")]
		public JsonResult AddView(string path, string title)
		{
			EnsureProjectLoaded(path);

			ZeroView view = this._zeroProjectsService.AddViewToProject(path, title);
			return Json(view);
		}

		[HttpGet("getcsv")]
		public JsonResult GetCSV(string path)
		{
			var csvDataConnector = new CSVDataConnector();
			string jsonResult = csvDataConnector.GetData(path);

			return Json(jsonResult);
		}

		[HttpGet("getexcel")]
		public JsonResult GetExcel(string path, string sheetName)
		{
			var excelDataConnector = new ExcelDataConnector();
			string jsonResult = excelDataConnector.GetData(path, sheetName);

			return Json(jsonResult);
		}

		private void EnsureProjectLoaded(string path)
		{
			// ensure project exists for the tesing purposes
			ZeroProject project = this._zeroProjectsService.GetProject(path);
			if (project == null)
			{
				project = this._zeroProjectsService.AddProject(path);
			}
		}
	}
}
