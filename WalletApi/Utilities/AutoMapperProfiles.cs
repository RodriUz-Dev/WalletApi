using AutoMapper;
using WalletApi.Application.DTOs.Request;
using WalletApi.Application.DTOs.Response;
using WalletApi.Domain.Entities;

namespace WalletApi.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            ConfigureWalletMapper();
            ConfigureTransactionHistoryMapper();
        }

        private void ConfigureWalletMapper()
        {
            CreateMap<CreateWalletRequestDTO, Wallet>();
            CreateMap<UpdateWalletRequestDTO, Wallet>();
            CreateMap<Wallet, WalletResponseDTO>();
            CreateMap<WalletResponseDTO, Wallet>();
            
        }

        private void ConfigureTransactionHistoryMapper()
        {
            CreateMap<TransactionHistoryRequestDTO, TransactionHistory>();
            CreateMap<TransactionHistory, TransactionHistoryResponseDTO>();
        }

    }
}
