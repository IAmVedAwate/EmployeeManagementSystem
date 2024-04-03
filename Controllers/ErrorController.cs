using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/Error/{statusCode}")]
        public IActionResult httpStatusShowcase(int statusCode)
        {   
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErMg = "Sorry!!  Page Not Found!";
                    
                    break;
                    
            }
            return View("Error");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error() {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionPath = exceptionDetails.Path;
            ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            ViewBag.ExceptionStackTrace = exceptionDetails.Error.StackTrace;
            return View("Error");
        }
    }
}
