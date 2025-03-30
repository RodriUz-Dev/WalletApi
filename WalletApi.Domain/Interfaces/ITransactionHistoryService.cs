
using WalletApi.Domain.Entities;

namespace WalletApi.Domain.Interfaces
{
    public interface ITransactionHistoryService
    {
        Task<List<TransactionHistory>> GetAllAsync();
        Task<TransactionHistory?> GetByIdAsync(int id);
        Task<TransactionHistory> CreateAsync(TransactionHistory transactionHistory);
        Task<int> UpdateAsync(int id, TransactionHistory transactionHistory);
        Task<int> DeleteAsync(int id);
    }
}
