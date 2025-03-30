using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;        
        private readonly IMapper _mapper;

        public WalletController(IWalletService walletService, IMapper mapper)
        {
            _walletService = walletService;            
            _mapper = mapper;
        }        

        /// <summary>
        /// Get all wallets
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var list = await _walletService.GetAllAsync();
            var walletList = _mapper.Map<List<WalletResponseDTO>>(list);            
            return StatusCode(StatusCodes.Status200OK, walletList);
        }

        /// <summary>
        /// Get a wallet by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var wallet = await _walletService.GetByIdAsync(id);
            var walletResponse = new WalletResponseDTO();
            if (wallet == null)
            {                
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, walletResponse });
            }
            walletResponse = _mapper.Map<WalletResponseDTO>(wallet);

            return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, walletResponse });
        }

        /// <summary>
        /// Create a new wallet
        /// </summary>
        /// <param name="walletDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateWalletRequestDTO walletDto) 
        {
            var wallet = _mapper.Map<Wallet>(walletDto);  

            if(wallet == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = false});
            }

            wallet.CreatedAt = DateTime.Now;

            await _walletService.CreateAsync(wallet);
           
            var walletResponse = _mapper.Map<WalletResponseDTO>(wallet);

            if (wallet.Id != 0)
            {
                return StatusCode(StatusCodes.Status201Created, new {isSuccess =true, value = walletResponse });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, value = walletResponse });
            }
        }

        /// <summary>
        /// Update a wallet
        /// </summary>
        /// <param name="id"></param>
        /// <param name="walletDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateWalletRequestDTO walletDto)
        {
            var wallet = _mapper.Map<Wallet>(walletDto);
            if (wallet == null || id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = false });
            }

            wallet.UpdatedAt = DateTime.Now;

            var result = await _walletService.UpdateAsync(id, wallet);
                        
            var walletResponse = _mapper.Map<WalletResponseDTO>(wallet);

            return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, value = walletResponse });

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if(id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = false });
            }

            var wallet = await _walletService.GetByIdAsync(id);

            if(wallet == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { isSuccess = false });
            }

            var walletDeleted = await _walletService.DeleteAsync(id);

            if(walletDeleted == 0)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
            }
           

            return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });

        }

    }
}
