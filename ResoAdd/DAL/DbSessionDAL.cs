using Dapper;
using Npgsql;
using ResoAdd.DAL.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ResoAdd.DAL
{
	public class DbSessionDAL : IDbSessionDAL
	{
		public async Task<int> Create(SessionModel model)
		{
			string sql = @"insert into DbSession (DbSessionID, SessionData, Created, LastAccessed, UserId)
                    values (@DbSessionID, @SessionContent, @Created, @LastAccessed, @UserId)";

			return await DbHelpper.ExecuteScalarAsync(sql, model);
		}


		public async Task<SessionModel?> Get(Guid sessionId)
		{
			string sql = @"select DbSessionID, SessionData, Created, LastAccessed, UserId from DbSession where DbSessionID = @sessionId";
			var sessions = await DbHelpper.QueryAsync<SessionModel>(sql, new { sessionId = sessionId });
			return sessions.FirstOrDefault();

		}
		public async Task Lock(Guid sessionId)
		{	
			string sql = @"select DbSessionID from DbSession where DbSessionID = @sessionId for update";
			await DbHelpper.QueryAsync<SessionModel>(sql, new { sessionId = sessionId });
		}
		public async Task<int> Update(SessionModel model)
		{
			string sql = @"update DbSession
								set SessionData = @SessionData, LastAccessed = @LastAccessed, UserId = @UserId
								Where  DbSessionID = @DbSessionID";
			return await DbHelpper.ExecuteScalarAsync(sql, model);
		}
	}
}
