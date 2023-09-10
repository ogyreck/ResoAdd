using ResoAdd.BL.Auth;
using ResoAdd.BL.Execptions;
using ResoTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ResoTest
{
	public class AuthTests:BaseTest
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public async Task AuthRegistrationTest()
		{
			using (TransactionScope scope = Helper.CreateTransactionScope())
			{
				string email = Guid.NewGuid().ToString() + "@test.com";


				//создание пользователя 
				int userId = await _authBL.CreateUser(
					new ResoAdd.DAL.Models.UserModel()
					{
						Email = email,
						Password = "qwerty1234",
					});

				Assert.Throws<AuthorizationException> (delegate { 
					_authBL.Authenticate(email, "111", false).GetAwaiter().GetResult();
				});

				Assert.Throws<AuthorizationException>(delegate {
					_authBL.Authenticate("qweqwe", "qwerty1234", false).GetAwaiter().GetResult();
				});

				Assert.Throws<AuthorizationException>(delegate {
					 _authBL.Authenticate("qweqwe", "qafsafay4", false).GetAwaiter().GetResult();
				});

				
				await _authBL.Authenticate(email, "qwerty1234", false);
			}
		}
	}
}
