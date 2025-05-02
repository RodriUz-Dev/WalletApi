using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using WalletApi.Application.DTOs.Request;
using WalletApi.Application.DTOs.Response;
using WalletApi.Application.Services;
using WalletApi.Controllers;
using WalletApi.Domain.Entities;
using WalletApi.Domain.Interfaces;
using WalletApi.Infrastructure.Persistence;
using WalletApi.Infrastructure.UnitOfWork;


namespace WalletUnitTest
{
    public class WalletTesting
    {

        private readonly IWalletService _walletService;
        private readonly WalletController _walletController;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public WalletTesting()
        {           
            var configuration = new ConfigurationBuilder();
            configuration.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            _configuration = configuration.Build();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(_configuration.GetConnectionString("ConnectionDB"))                
                .Options;

            // Create a new instance of ApplicationDbContext with the options
            var context = new ApplicationDbContext(options);

            // Initialize the UnitOfWork with the context
            _unitOfWork = new UnitOfWork(context);
            
            //// Initialize AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateWalletRequestDTO, Wallet>();
                cfg.CreateMap<UpdateWalletRequestDTO, Wallet>();
                cfg.CreateMap<Wallet, WalletResponseDTO>();
                cfg.CreateMap<WalletResponseDTO, Wallet>();

            });

            // Initialize _walletService and _walletController as needed
            var mapper = config.CreateMapper();
            _walletService = new WalletService(_unitOfWork); // Adjust this line based on your actual implementation
            _walletController = new WalletController(_walletService, mapper); // Assuming you have a valid IMapper instance
                    
        }

        [Fact]
        public async Task GetAllAsync()
        {  
            // Act        
            var result = await _walletController.GetList();
            var okObjectResult = (ObjectResult)result;
            var walletList = Assert.IsType<List<WalletResponseDTO>>(okObjectResult.Value);
            
            //Asert
            Assert.NotNull(result);            
            Assert.True(walletList.Count > 0, "Wallet list should not be empty.");                        
        }
    }
}
