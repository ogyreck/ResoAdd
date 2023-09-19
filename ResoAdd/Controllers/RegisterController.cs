using Microsoft.AspNetCore.Mvc;
using ResoAdd.BL.Auth;
using ResoAdd.ViewModels;
using ResoAdd.ViewMapper;
using ResoAdd.BL.Execptions;

namespace ResoAdd.Controllers 
{
    public class RegisterController: Controller
    {

        private readonly IAuth _authBL;
        public RegisterController(IAuth authBL)
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
        public async Task<IActionResult> IndexSave(RegisterViewModel model)
        {
            

			if (ModelState.IsValid)
            {
                try
                {
					await _authBL.Register(AuthMapper.MapRegisterViewModelToUserModel(model));
					return Redirect("/");
				}
                catch(DublicatEmailException)
                {
					ModelState.TryAddModelError("Email", "Email уже существует");
				}
			
				
			}
			return View("Index", model);
        }
    }
}
