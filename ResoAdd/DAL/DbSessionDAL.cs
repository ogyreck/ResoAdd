using Dapper;
using Npgsql;
using ResoAdd.DAL.Models;

namespace ResoAdd.DAL
{
	public class DbSessionDAL : IDbSessionDAL
	{
		public async Task<int> CreateSession(SessionModel model)
		{
			using (var connection = new NpgsqlConnection(DbHelpper.ConnString))
			{
				await connection.OpenAsync();
				string sql = @"insert into DbSession (DbSessionID, SessionData, Created, LastAccessed, UserId)
                      values (@DbSessionID, @SessionContent, @Created, @LastAccessed, @UserId)";

				return await connection.ExecuteAsync(sql, model);
			}
		}

		public async Task<SessionModel?> GetSession(Guid sessionId)
		{
			using (var connection = new NpgsqlConnection(DbHelpper.ConnString))
			{
				await connection.OpenAsync();
				string sql = @"select DbSessionID, SessionData, Created, LastAccessed, UserId from DbSession where DbSessionID = @sessionId";

				var sessions = await connection.QueryAsync<SessionModel>(sql, new { sessionId = sessionId });
				return sessions.FirstOrDefault();
			}
		}

		public async Task<int> UpdateSession(SessionModel session)
		{
			using (var connection = new NpgsqlConnection(DbHelpper.ConnString))
			{
				await connection.OpenAsync();

				string sql = @"update DbSession
								set SessionData = @SessionData, LastAccessed = @LastAccessed, UserId = @UserId
								Where  DbSessionID = @DbSessionID";

				return await connection.ExecuteAsync(sql, session);
			}
		}
	}
}
