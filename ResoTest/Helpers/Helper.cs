using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ResoTest.Helpers
{
	public static class Helper
	{
		public static TransactionScope CreateTransactionScope(int seconds =1)
		{
			return new TransactionScope(
				TransactionScopeOption.Required,
				new TimeSpan(0, 0, seconds),
				TransactionScopeAsyncFlowOption.Enabled);
		}
	}
}
