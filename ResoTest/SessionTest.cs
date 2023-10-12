using ResoTest.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ResoTest
{
	public  class SessionTest: Helpers.BaseTest
	{
		[Test]
		public async Task CreateSessionTest()
		{
			using(TransactionScope scope  = Helper.CreateTransactionScope())
			{
				var session = await _dbSession.GetSession();

				var dbSession = await _dbSessionDAL.Get(session.DbSessionId);

				Assert.NotNull(dbSession);

				Assert.That(dbSession.DbSessionId, Is.EqualTo(session.DbSessionId));

				var session2 = await _dbSession.GetSession();

				Assert.That(dbSession.DbSessionId, Is.EqualTo(session2.DbSessionId));

			}
		}
	}
}
