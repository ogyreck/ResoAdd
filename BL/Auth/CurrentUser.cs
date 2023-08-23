namespace ResoAdd.BL.Auth
{
	public class CurrentUser : ICurrentUser
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		public CurrentUser(IHttpContextAccessor httpContextAccessor) { 
				_httpContextAccessor = httpContextAccessor;
		}
		public bool ISLoggedIn()
		{
			int? id = _httpContextAccessor.HttpContext?.Session.GetInt32(AuthConstant.AUTH_SESSION_PARAM_KEY);

            return  id != null;
		}
	}
}
