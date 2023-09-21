using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace TestProject.Controllers
{
    public class HomeController : Controller
    {
        internal static List<KeyValuePair<string, string>> allMessages = new List<KeyValuePair<string, string>>();
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("ID") is null)
            {
                HttpContext.Session.SetString("ID", HttpContext.Session.Id);
                HttpContext.Session.SetString("Messages", JsonSerializer.Serialize<List<string>>(new List<string>()));
            }

            return View();
        }
        [HttpPost]
        public IActionResult Send(string msg)
        {
            AddMessage(msg);
            return Redirect("Index");
        }
        private void AddMessage(string msg)
        {
            var value = HttpContext.Session.GetString("Messages");
            List<string> msgs = value is null ? new List<string>() : JsonSerializer.Deserialize<List<string>>(value);
            if (msgs.Count == 10)
            {
                msgs.RemoveAt(0);
            }
            msgs.Add(msg);
            HttpContext.Session.SetString("Messages", JsonSerializer.Serialize(msgs));
            var l = HttpContext.Session.GetString("Messages");

            if (allMessages.Count == 20)
            {
                allMessages.RemoveAt(0);
            }
            allMessages.Add(new KeyValuePair<string, string>(HttpContext.Session.GetString("ID"), msg));
        }
    }
}