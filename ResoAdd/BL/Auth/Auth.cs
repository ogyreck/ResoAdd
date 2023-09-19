using ResoAdd.DAL.Models;
using ResoAdd.DAL;
using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;

using ResoAdd.BL.Execptions;
using ResoAdd.BL.General;

namespace ResoAdd.BL.Auth
{
    /// <summary>
    /// Реализация BL уровня
    /// </summary>
    public class Auth : IAuth
    {
        /// <summary>
        /// Зависимость от authDAL
        /// </summary>
        private readonly IAuthDAL _authDAL;
        private readonly IEncrypt _encrypt;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDbSession _dbSession;
        public Auth(IAuthDAL authDAL, IEncrypt encrypt, IHttpContextAccessor httpContextAccessor, IDbSession dbSession)
        {
            _authDAL = authDAL;
			_encrypt = encrypt;
			_httpContextAccessor = httpContextAccessor;
            _dbSession = dbSession;

		}

		public async Task<int> Authenticate(string email, string password, bool rememberMe)
		{
			UserModel user = await _authDAL.GetUser(email);
            if(user.UserId == null)
            {
				throw new AuthorizationException();
			}
            if(user.Password == _encrypt.HashPassword(password, user.Salt))
            {
                await Login(user.UserId?? 0);
                return user.UserId ?? 0;
            }
            throw new AuthorizationException();
		}

		/// <summary>
		///  Создание юзера на уровне BL
		/// </summary>
		/// <param name="userModel"></param>
		/// <returns></returns>
		public async Task<int> CreateUser(UserModel userModel)
        {
            userModel.Salt = Guid.NewGuid().ToString();
            userModel.Password = _encrypt.HashPassword(userModel.Password, userModel.Salt);
            int id =  await _authDAL.CreateUser(userModel);
            await Login(id);
            return id;
        }
        public async Task Login(int id)
        {
            await _dbSession.SetUserId(id);

		}

        public async Task ValidateEmail(string email)
        {
            var user = await _authDAL.GetUser(email);

            if(user.UserId != null)
            {
                throw new DublicatEmailException();
            }
           
        }

		public async Task Register(UserModel userModel)
		{
            using (var scope = Helpers.CreateTransactionScope())
            {
                await _dbSession.Lock();
				await ValidateEmail(userModel.Email);
				await CreateUser(userModel);
                scope.Complete();
			}

		}
            
	}
}
