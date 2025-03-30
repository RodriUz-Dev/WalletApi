using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WalletApi.Domain.Entities;
using WalletApi.Infrastructure.Persistence;

namespace WalletApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private ApplicationDbContext _context;

        public WalletController(ApplicationDbContext dbContext)
        {
            _context = dbContext;            
        }        

        /// <summary>
        /// Get all wallets
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var list = await _context.Wallets.ToListAsync();
            return StatusCode(StatusCodes.Status200OK,  list );
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

            var wallet = await _context.Wallets.FirstOrDefaultAsync(x => x.Id == id);
            if (wallet == null)
            {
                wallet = new Wallet();
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false,  wallet });
            }

            return StatusCode(StatusCodes.Status200OK, new { isSuccess = true,  wallet });
        }

        /// <summary>
        /// Create a new wallet
        /// </summary>
        /// <param name="walletDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Wallet walletDto) //Cambiar por DTO
        {
            var wallet = walletDto;  //mappear 

            if(wallet == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = false});
            }

            wallet.CreatedAt = DateTime.Now;

            await _context.Wallets.AddAsync(wallet);
            await _context.SaveChangesAsync();

            if (wallet.Id != 0)
            {
                return StatusCode(StatusCodes.Status201Created, new {isSuccess =true, value = wallet });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, value = wallet });
            }
        }

        /// <summary>
        /// Update a wallet
        /// </summary>
        /// <param name="id"></param>
        /// <param name="walletDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] Wallet walletDto)
        {
            var wallet = walletDto;
            if (wallet == null || id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = false });
            }

            var result = _context.Wallets.Update(wallet);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, value = result });

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if(id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = false });
            }

            var wallet = _context.Wallets.FirstOrDefault(x => x.Id == id);

            if(wallet == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { isSuccess = false });
            }

            var walletDeleted = await _context.Wallets.Where(wallet => wallet.Id == id).ExecuteDeleteAsync();

            if(walletDeleted == 0)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
            }

            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });

        }

    }
}
