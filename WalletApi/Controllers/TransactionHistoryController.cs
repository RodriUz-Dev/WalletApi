using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WalletApi.Application.DTOs.Request;
using WalletApi.Application.DTOs.Response;
using WalletApi.Application.Services;
using WalletApi.Domain.Entities;
using WalletApi.Domain.Interfaces;
using WalletApi.Infrastructure.Persistence;

namespace WalletApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionHistoryController : ControllerBase
    {
        private readonly ITransactionHistoryService _transactionHistoryService;
        private readonly IWalletService _walletService;        
        private readonly IMapper _mapper;
        public TransactionHistoryController(ITransactionHistoryService transactionHistoryService, IWalletService walletService, IMapper mapper)
        {
            _transactionHistoryService = transactionHistoryService;
            _walletService = walletService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var list = await _transactionHistoryService.GetAllAsync();
            var transactionList = _mapper.Map<List<TransactionHistoryResponseDTO>>(list);
            return StatusCode(StatusCodes.Status200OK, transactionList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var transactionHistory = await _transactionHistoryService.GetByIdAsync(id);
            var transactionHistoryResponse = new TransactionHistoryResponseDTO();
            if (transactionHistory == null)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, transactionHistoryResponse });
            }
            transactionHistoryResponse = _mapper.Map<TransactionHistoryResponseDTO>(transactionHistory);

            return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, transactionHistoryResponse });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TransactionHistoryRequestDTO transactionHistoryRequestDTO)
        {
            var transactionHistory = _mapper.Map<TransactionHistory>(transactionHistoryRequestDTO);

            if (transactionHistory == null || transactionHistory.Amount <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = false });
            }                       

            Wallet? wallet = await _walletService.GetByIdAsync(transactionHistory.WalletId);

            TransactionHistoryResponseDTO transactionHistoryResponse = new TransactionHistoryResponseDTO();
            if (wallet is not null)
            {
                if (transactionHistory.Type.ToLower() == "debit")
                {
                    wallet.Balance += transactionHistory.Amount;
                }
                else if (transactionHistory.Type.ToLower() == "credit")
                {
                    if (transactionHistory.Amount > wallet.Balance)
                    {
                        return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, value = transactionHistoryResponse });
                    }
                    wallet.Balance -= transactionHistory.Amount;
                }

                transactionHistory.CreatedAt = DateTime.Now;


                await _transactionHistoryService.CreateAsync(transactionHistory);
                
                transactionHistoryResponse = _mapper.Map<TransactionHistoryResponseDTO>(transactionHistory);

                if (transactionHistoryResponse.Id != 0)
                {
                    var result = _walletService.UpdateAsync(transactionHistory.WalletId, wallet);
                    
                    return StatusCode(StatusCodes.Status201Created, new { isSuccess = true, value = transactionHistoryResponse });
                }
                else
                {

                    return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, value = transactionHistoryResponse });
                }
            }

            return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, value = transactionHistoryResponse });


        }

    }
}
