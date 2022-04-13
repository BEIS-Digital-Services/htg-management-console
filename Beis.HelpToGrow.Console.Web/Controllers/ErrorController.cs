namespace Beis.HelpToGrow.Console.Web.Controllers
{
    using System.Diagnostics;
    
    using Beis.HelpToGrow.Console.Web.Models;
    using Microsoft.AspNetCore.Mvc;

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