using Microsoft.AspNetCore.Http;
using ResoAdd.BL.General;
using ResoAdd.DAL;

namespace ResoAdd.BL.Auth
{
	public class CurrentUser : ICurrentUser
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IDbSession _dbSession;
		private readonly IWebCookie _webCookie;
		private readonly IUserTokenDAL _userTokenDAL;

		public CurrentUser(IHttpContextAccessor httpContextAccessor,
			IDbSession dbSession,
			IWebCookie webCookie,
			IUserTokenDAL userTokenDAL

			) 
		{ 
				_httpContextAccessor = httpContextAccessor;
				_dbSession = dbSession;
				_webCookie = webCookie;
			    _userTokenDAL = userTokenDAL;
		}

		public async Task<int?> GetUserIdByToken()
		{
			
			string? tokenCookie = _webCookie.Get(AuthConstant.RememberMeCookieName);
			if(tokenCookie == null)
			{
				return null;
			}
			Guid? tokenGuid = Helpers.StringToGuidDef(tokenCookie ?? "");
			if(tokenGuid == null)
			{
				return null;
			}
			int? userid = await _userTokenDAL.Get((Guid)tokenGuid);
			return userid;
		}

		public async Task<bool> ISLoggedIn()
		{

			bool isLoggedIn =  await _dbSession.IsLoggedIn();
			if (!isLoggedIn)
			{
				int? userid  = await GetUserIdByToken();
				if(userid != null)
				{
					await _dbSession.SetUserId((int)userid);
					isLoggedIn = true;
				}
			}
			return isLoggedIn;
		} 
	}
}
