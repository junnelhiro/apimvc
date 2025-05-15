using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplicationmvc.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplicationmvc.Controllers
{
    public class EmployeeController : Controller
    {
       public IActionResult Index()
        {
            return View();
        }
    }
}
