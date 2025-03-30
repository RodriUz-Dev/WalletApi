using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApi.Domain.Entities;
using WalletApi.Domain.Interfaces;

namespace WalletApi.Application.Services
{
    public class TransactionHistoryService : ITransactionHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TransactionHistoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;            
        }

        public async Task<TransactionHistory> CreateAsync(TransactionHistory transactionHistory)
        {
            var _repository = _unitOfWork.Repository<TransactionHistory>();

            await _repository.Add(transactionHistory);
            await _unitOfWork.CommitAsync();
            return transactionHistory;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var _repository = _unitOfWork.Repository<TransactionHistory>();
            var transactionHistory = await _repository.GetByIdAsync(id);
            if (transactionHistory == null)
            {
                return 0;
            }
            _repository.Delete(transactionHistory);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<List<TransactionHistory>> GetAllAsync()
        {
            var _repository = _unitOfWork.Repository<TransactionHistory>();
            return (List<TransactionHistory>) await _repository.GetAllAsync();
        }

        public async Task<TransactionHistory?> GetByIdAsync(int id)
        {
            var _repository = _unitOfWork.Repository<TransactionHistory>();
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, TransactionHistory transactionHistory)
        {
            var _repository = _unitOfWork.Repository<TransactionHistory>();
            var _transactionHistory = await _repository.GetByIdAsync(id);
            if (_transactionHistory == null)
            {
                return 0;
            }

            _transactionHistory.WalletId = transactionHistory.WalletId;
            _transactionHistory.Amount = transactionHistory.Amount;
            _transactionHistory.CreatedAt = transactionHistory.CreatedAt;
            _transactionHistory.Type = transactionHistory.Type;

            _repository.Update(_transactionHistory);
            return await _unitOfWork.CommitAsync();
        }
    }
    
}
