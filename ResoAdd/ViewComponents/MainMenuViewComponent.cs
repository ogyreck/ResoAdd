using Microsoft.AspNetCore.Mvc;
using ResoAdd.BL.Auth;

namespace ResoAdd.ViewComponents
{
	public class MainMenuViewComponent: ViewComponent
	{
		private readonly ICurrentUser _currentUser;
        public MainMenuViewComponent(ICurrentUser currentUser)
        {
			_currentUser = currentUser;
        }

        public async Task<IViewComponentResult> InvokeAsync()
		{
			bool isLoggedIn = await _currentUser.ISLoggedIn();
			return View("Index", isLoggedIn);
		}
	}
}
