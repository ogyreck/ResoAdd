using Microsoft.AspNetCore.Mvc;
using ResoAdd.BL.Auth;
using ResoAdd.BL.Execptions;
using ResoAdd.ViewModels;

namespace ResoAdd.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthBL _authBL;
        public LoginController(IAuthBL authBL)
        {
            _authBL = authBL;
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Index()
        {
            return View("Index", new LoginViewModel());
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> IndexSave(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try { 
                    await _authBL.Authenticate(model.Email!, model.Password!, model.RememberMe == true);
					return Redirect("/");
				}
                catch (AuthorizationException)
                {
                    ModelState.AddModelError("Email", "Имя или Email неверный");
                }

					
            }
            return View("Index", model);
        }
    }
}
