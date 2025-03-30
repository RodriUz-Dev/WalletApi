
using WalletApi.Domain.Entities;
using WalletApi.Domain.Interfaces;

namespace WalletApi.Application.Services
{
    public class WalletService : IWalletService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WalletService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Wallet> CreateAsync(Wallet wallet)
        {
            var _repository = _unitOfWork.Repository<Wallet>();

            await _repository.Add(wallet);
            await _unitOfWork.CommitAsync();
            return wallet;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var _repository = _unitOfWork.Repository<Wallet>();
            var wallet = await _repository.GetByIdAsync(id);
            if (wallet == null)
            {
                return 0;
            }
            _repository.Delete(wallet);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<List<Wallet>> GetAllAsync()
        {
            var _repository = _unitOfWork.Repository<Wallet>();
            return (List<Wallet>) await _repository.GetAllAsync();
        }

        public async Task<Wallet?> GetByIdAsync(int id)
        {
            var _repository = _unitOfWork.Repository<Wallet>();
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, Wallet wallet)
        {
            var _repository = _unitOfWork.Repository<Wallet>();
            var _wallet = await _repository.GetByIdAsync(id);
            if (wallet == null)
            {
                return 0;
            }
            
            _wallet.DocumentId = wallet.DocumentId;
            _wallet.Name = wallet.Name;
            _wallet.UpdatedAt = wallet.UpdatedAt;            
            _wallet.Balance = wallet.Balance;      

            _repository.Update(_wallet);
            return await _unitOfWork.CommitAsync();
        }
    }
}
