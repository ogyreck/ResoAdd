using Microsoft.AspNetCore.Mvc;
using ResoAdd.BL.Auth;
using ResoAdd.ViewModels;
using ResoAdd.ViewMapper;


namespace ResoAdd.Controllers 
{
    public class RegisterController: Controller
    {

        private readonly IAuthBL _authBL;
        public RegisterController(IAuthBL authBL)
        {
            _authBL = authBL;
        }

        [HttpGet]
        [Route("/register")]
        public IActionResult Index()
        {
            return View("Index", new RegisterViewModel());
        }

        [HttpPost]
        [Route("/register")]
        public IActionResult IndexSave(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                _authBL.CreateUser(AuthMapper.MapRegisterViewModelToUserModel(model));
                return Redirect("/");
            }
            return View("Index", model);
        }
    }
}
