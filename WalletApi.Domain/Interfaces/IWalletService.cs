
using WalletApi.Domain.Entities;

namespace WalletApi.Domain.Interfaces
{
    public interface IWalletService
    {
        Task<List<Wallet>> GetAllAsync();
        Task<Wallet?> GetByIdAsync(int id);
        Task<Wallet> CreateAsync(Wallet wallet);
        Task<int> UpdateAsync(int id, Wallet wallet);
        Task<int> DeleteAsync(int id);
    }
}
