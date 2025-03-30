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
    public class WalletRepository : Repository<Wallet>, IWalletRepository
    {
        public WalletRepository(ApplicationDbContext context): base(context) 
        {
            
        }
    }
}
