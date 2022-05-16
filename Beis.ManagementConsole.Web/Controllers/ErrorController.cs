using Beis.ManagementConsole.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Beis.ManagementConsole.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}