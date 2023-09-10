using ResoAdd.BL.Auth;
using ResoAdd.DAL;
using ResoAdd.DAL.Models;
using ResoTest.Helpers;
using System.Transactions;

namespace ResoTest
{
	public class RegistersTests : BaseTest
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public async Task BaseRegistrationTest()
		{
			using (TransactionScope scope = Helper.CreateTransactionScope())
			{
				string email = Guid.NewGuid().ToString() + "@test.com";

				// validate: should not be in the DB
				var emailValidationResult = await _authBL.ValidateEmail(email);
				Assert.IsNull(emailValidationResult);

				// create user
				int userId = await _authBL.CreateUser(
					new UserModel()
					{
						Email = email,
						Password = "qwer1234"
					});

				Assert.Greater(userId, 0);

				var userdalresult = await _authDAL.GetUser(userId);
				Assert.That(email, Is.EqualTo(userdalresult.Email));
				Assert.NotNull(userdalresult.Salt);

				var userbyemaildalresult = await _authDAL.GetUser(email);
				Assert.That(email, Is.EqualTo(userbyemaildalresult.Email));

				// validate: should be in the DB
				emailValidationResult = await _authBL.ValidateEmail(email);
				Assert.IsNotNull(emailValidationResult);

				string encpassword = _encrypt.HashPassword("qwer1234", userbyemaildalresult.Salt);
				Assert.That(encpassword, Is.EqualTo(userbyemaildalresult.Password));
			}
		}
	}
}