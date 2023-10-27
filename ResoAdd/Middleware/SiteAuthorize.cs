using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ResoAdd.BL.Auth;

namespace ResoAdd.Middleware
{
	public class SiteAuthorize
	{
		[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
		public class SiteAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
		{
			public SiteAuthorizeAttribute()
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
				if (isLoggedIn == false)
				{
					context.Result = new RedirectResult("/Login");
					return;
				}
			}
		}
	}
}
