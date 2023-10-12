using ResoAdd.DAL.Models;

namespace ResoAdd.DAL
{
	public interface IUserTokenDAL
	{
		Task<Guid> Create(int userID);
		Task<int?> Get(Guid tokenID);



	}
}
