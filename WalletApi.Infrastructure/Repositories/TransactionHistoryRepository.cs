using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApi.Domain.Entities;
using WalletApi.Domain.Interfaces;
using WalletApi.Infrastructure.Persistence;

namespace WalletApi.Infrastructure.Repositories
{
    public class TransactionHistoryRepository : Repository<TransactionHistory>, ITransactionHistoryRepository
    {
        public TransactionHistoryRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
