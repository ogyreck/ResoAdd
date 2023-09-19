using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using ResoAdd.DAL;
using ResoAdd.DAL.Models;

namespace ResoAdd.BL.Auth
{
	public class DbSession : IDbSession
	{

		private readonly IDbSessionDAL _sessionDAL;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public DbSession(IDbSessionDAL sessionDAL, IHttpContextAccessor httpContextAccessor)
		{
			_sessionDAL = sessionDAL;
			_httpContextAccessor = httpContextAccessor;
		}
		private void CreateSectionCookie(Guid sessionId)
		{
			CookieOptions options = new CookieOptions();
			options.Path = "/";
			options.HttpOnly = true;
			options.Secure = true;
			_httpContextAccessor?.HttpContext?.Response.Cookies.Delete(AuthConstant.SessionCookieName);
			_httpContextAccessor?.HttpContext?.Response.Cookies.Append(AuthConstant.SessionCookieName, sessionId.ToString(), options);

		}


		private async Task<SessionModel> CreateSession()
		{
			var data = new SessionModel()
			{
				DbSessionId = Guid.NewGuid(),
				Created = DateTime.Now,
				LastAccessed = DateTime.Now
			};
			await _sessionDAL.Create(data);
			return data;
		}

		private SessionModel? sessionModel = null;
		public async Task<SessionModel> GetSession()
		{
			if (sessionModel != null)
				return sessionModel;

			Guid sessionId;
			var cookie = _httpContextAccessor?.HttpContext?.Request?.Cookies.FirstOrDefault(m => m.Key == AuthConstant.SessionCookieName);
			if (cookie != null && cookie.Value.Value != null)
				sessionId = Guid.Parse(cookie.Value.Value);
			else
				sessionId = Guid.NewGuid();

			var data = await _sessionDAL.Get(sessionId);
			if (data == null)
			{
				data = await this.CreateSession();
				CreateSectionCookie(data.DbSessionId);
			}
			sessionModel = data;
			return data;
		}

		public async Task<int?> GetUserId()
		{
			var data = await GetSession();
			return data.UserId;
		}

		public async Task<bool> IsLoggedIn()
		{
			var data = await GetSession();
			return data.UserId != null;
		}

		public async Task<int> SetUserId(int userId)
		{
			var data = await this.GetSession();
			data.UserId = userId;
			data.DbSessionId = Guid.NewGuid();
			CreateSectionCookie(data.DbSessionId);
			return await _sessionDAL.Create(data);
		}

		public async Task Lock()
		{
			var data = await GetSession();
			await _sessionDAL.Lock(data.DbSessionId);
		}
	}
}
