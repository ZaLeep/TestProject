using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace TestProject.Controllers
{
    public class MessagesPageController : Controller
    {
        public IActionResult Index()
        {
            var value = HttpContext.Session.GetString("Messages");
            if (value is null) 
            { 
                ViewBag.ownMessages = new List<string>(); 
            }
            else
            {
                ViewBag.ownMessages = JsonSerializer.Deserialize<List<string>>(value);
            }
            return View(HomeController.allMessages);
        }
    }
}
