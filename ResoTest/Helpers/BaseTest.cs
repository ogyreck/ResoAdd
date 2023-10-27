using System;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using ResoAdd.BL.Auth;
using ResoAdd.BL.General;
using ResoAdd.DAL;
using Resutest.Helpers;

namespace ResoTest.Helpers
{

	
	public class BaseTest
	{
		protected IAuthDAL _authDAL = new AuthDAL();
		protected IEncrypt _encrypt = new Encrypt();
		protected IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
		protected IDbSessionDAL _dbSessionDAL = new DbSessionDAL();
		protected IDbSession _dbSession;
		protected IAuth _authBL;
		protected IWebCookie webCookie;
		protected IUserTokenDAL _userTokenDAL = new UserTokenDAL();
		protected ICurrentUser currentUser;


		public BaseTest() {
			webCookie = new TestCookie();
			_dbSession = new DbSession(_dbSessionDAL, webCookie);
			_authBL = new Auth(_authDAL, _encrypt, _httpContextAccessor, _dbSession,_userTokenDAL,webCookie);
			currentUser = new CurrentUser(_dbSession, webCookie, _userTokenDAL);

		}

	}
}
