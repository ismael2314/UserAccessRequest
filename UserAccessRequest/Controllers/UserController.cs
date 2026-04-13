using Microsoft.AspNetCore.Mvc;

namespace UserAccessRequest.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
