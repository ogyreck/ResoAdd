using ResoAdd.DAL.Models;
using ResoAdd.DAL;

namespace ResoAdd.BL.Auth
{
    /// <summary>
    /// Реализация BL уровня
    /// </summary>
    public class AuthBL : IAuthBL
    {
        /// <summary>
        /// Зависимость от authDAL
        /// </summary>
        private readonly IAuthDAL _authDAL;
        public AuthBL(IAuthDAL authDAL)
        {
            _authDAL = authDAL;
        }
        /// <summary>
        ///  Создание юзера на уровне BL
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<int> CreateUser(UserModel userModel)
        {
            return await _authDAL.CreateUser(userModel);
        }
    }
}
