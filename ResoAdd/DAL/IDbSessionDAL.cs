using ResoAdd.DAL.Models;

namespace ResoAdd.DAL
{
	public interface IDbSessionDAL
	{
		Task<SessionModel?> Get(Guid sessionID);
		Task<int> Update(SessionModel session);
		Task<int> Create(SessionModel session);
		Task Lock(Guid sessionId);
	}
}
