using System;
using Microsoft.AspNetCore.Http;
using ResoAdd.BL.Auth;
using ResoAdd.DAL;

namespace ResoTest.Helpers
{

	
	public class BaseTest
	{
		protected IAuthDAL _authDAL = new AuthDAL();
		protected IEncrypt _encrypt = new Encrypt();
		protected IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
		protected IDbSessionDAL _dbSessionDAL = new DbSessionDAL();
		protected IDbSession _dbSession;
		protected IAuthBL _authBL;
		

		public BaseTest() {
			_dbSession = new DbSession(_dbSessionDAL, _httpContextAccessor);
			_authBL = new AuthBL(_authDAL, _encrypt, _httpContextAccessor, _dbSession);
			

		}

	}
}
