using Microsoft.AspNet.Mvc;
using ProjectZero.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers.Api
{
	[Route("api/test")]
    public class TestController : Controller
    {
		[HttpGet()]
		public JsonResult Get(string path)
		{
			//var path = @"C:\Work\Samples\Web\WebApplication\src\TestClassLibrary";

			var aa = new ZeroProject();
			var result = aa.Load(path);

			return Json(result);
		}
    }
}
