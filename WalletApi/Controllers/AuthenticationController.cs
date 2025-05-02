using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WalletApi.Application.Custom;
using WalletApi.Application.DTOs.Request;
using WalletApi.Application.DTOs.Response;
using WalletApi.Domain.Entities;
using WalletApi.Infrastructure.Persistence;

namespace WalletApi.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly JwtUtilities _jwtUtilities;
        //private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        public AuthenticationController(ApplicationDbContext dbContext, JwtUtilities jwtUtilities, IMapper mapper)
        {
            _dbContext = dbContext;
            _jwtUtilities = jwtUtilities;
            //_authenticationService = authenticationService;
            _mapper = mapper;

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            //var user = await _authenticationService.LoginAsync(loginRequestDTO);
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == loginRequestDTO.Email && u.Password == _jwtUtilities.encryptSHA256(loginRequestDTO.Password!));
            if (user == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { isSuccess = false });
            }
           
            var token = _jwtUtilities.generateJWT(user);                        
            var userResponse = _mapper.Map<UserResponseDTO>(user);
            userResponse.Token = token;
            
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, userResponse });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            //var user = await _authenticationService.RegisterAsync(registerRequestDTO);
            var user = _mapper.Map<User>(registerRequestDTO);            

            if (user == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = false });
            }
            user.Password = _jwtUtilities.encryptSHA256(registerRequestDTO.Password!);
            user.CreatedAt = DateTime.Now;

            var existingUser = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == registerRequestDTO.Email);

            if (existingUser != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = false, message = "User already exists." });
            }
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();          

            var userResponse = _mapper.Map<UserResponseDTO>(user);
            return StatusCode(StatusCodes.Status201Created, new { isSuccess = true, userResponse });
        }

        [HttpGet("validateToken")]
        public IActionResult ValidateToken([FromQuery] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = false });
            }
            var isValid = _jwtUtilities.validateToken(token);
            if (!isValid)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { isSuccess = false });
            }
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
        }
    }
}
