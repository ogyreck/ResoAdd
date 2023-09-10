using ResoAdd.DAL.Models;

namespace ResoAdd.DAL
{
	public interface IDbSessionDAL
	{
		Task<SessionModel?> GetSession(Guid sessionID);
		Task<int> UpdateSession(SessionModel session);
		Task<int> CreateSession(SessionModel session);
	}
}
