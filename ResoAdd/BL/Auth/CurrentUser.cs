using Microsoft.AspNetCore.Http;

namespace ResoAdd.BL.Auth
{
	public class CurrentUser : ICurrentUser
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IDbSession _dbSession;
		public CurrentUser(IHttpContextAccessor httpContextAccessor, IDbSession dbSession) { 
				_httpContextAccessor = httpContextAccessor;
				_dbSession = dbSession;
		}
		public async Task<bool> ISLoggedIn()
		{
			var data = await _dbSession.IsLoggedIn();
			return data;
		}
	}
}
