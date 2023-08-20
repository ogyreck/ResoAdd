 using ResoAdd.DAL.Models;

namespace ResoAdd.BL.Auth
{
    public interface IAuthBL
    {
        Task<int> CreateUser(UserModel userModel);
    }
}
