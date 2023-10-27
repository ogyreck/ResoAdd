using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ResoAdd.BL.Auth;

namespace ResoAdd.Middleware
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class SiteNotAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
	{
		public SiteNotAuthorizeAttribute()
		{
		}

		public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
		{
			ICurrentUser? currentUser = context.HttpContext.RequestServices.GetService<ICurrentUser>();
			if (currentUser == null)
			{
				throw new Exception("No user middleware");
			}

			bool isLoggedIn = await currentUser.ISLoggedIn();
			if (isLoggedIn == true)
			{
				context.Result = new RedirectResult("/");
				return;
			}
		}
	}
}
