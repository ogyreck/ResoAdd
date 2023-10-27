using ResoAdd.BL.Auth;
using ResoAdd.DAL.Models;
using ResoTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ResoTest
{
	public class CurrentUserTest : Helpers.BaseTest
	{
		[Test]
		public async Task BaseRegistrationTest()
		{
			using (TransactionScope scope = Helper.CreateTransactionScope())
			{
				await CreateAndAuthUser();

				bool isLoggedIn = await this.currentUser.ISLoggedIn();
				Assert.True(isLoggedIn);

				webCookie.Delete(AuthConstant.SessionCookieName);
				_dbSession.ResetSessionCache();

				isLoggedIn = await this.currentUser.ISLoggedIn();
				Assert.True(isLoggedIn);

				webCookie.Delete(AuthConstant.SessionCookieName);
				webCookie.Delete(AuthConstant.RememberMeCookieName);
				_dbSession.ResetSessionCache();

				isLoggedIn = await this.currentUser.ISLoggedIn();
				Assert.False(isLoggedIn);
			}
		}

		public async Task<int> CreateAndAuthUser()
		{
			string email = Guid.NewGuid().ToString() + "@test.com";

			// create user
			int userId = await _authBL.CreateUser(
				new UserModel()
				{
					Email = email,
					Password = "qwer1234"
				});
			return await _authBL.Authenticate(email, "qwer1234", true);
		}
	}
}
