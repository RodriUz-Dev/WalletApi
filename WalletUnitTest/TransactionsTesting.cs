using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class TransactionsTesting
    {
        private readonly ITransactionHistoryService _transactionHistoryService;
        private readonly IWalletService _walletService;
        private readonly TransactionHistoryController _transactionHistoryController;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public TransactionsTesting()
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
                cfg.CreateMap<TransactionHistoryRequestDTO, TransactionHistory>();                           

                cfg.CreateMap<TransactionHistory, TransactionHistoryResponseDTO>();
                cfg.CreateMap<TransactionHistoryResponseDTO, TransactionHistory>();
            });
            // Initialize _transactionHistoryService and _transactionHistoryController as needed
            var mapper = config.CreateMapper();
            _transactionHistoryService = new TransactionHistoryService(_unitOfWork); // Adjust this line based on your actual implementation
            _walletService = new WalletService(_unitOfWork); // Adjust this line based on your actual implementation
            _transactionHistoryController = new TransactionHistoryController(_transactionHistoryService, _walletService, mapper); // Assuming you have a valid IMapper instance
        }

        [Fact]
        public async Task GetTransactionHistoryById_ValidId_ReturnsTransactionHistory()
        {
            // Arrange
            var transactionHistoryId = 2; // Replace with a valid ID from your test database
            // Act
            var result = await _transactionHistoryController.GetById(transactionHistoryId);
                
            var okResult = (ObjectResult)result;
            var valueResult = okResult.Value;
            var transactionHistoryResponse = Assert.IsType<TransactionResponseDTO>(valueResult);
            // Assert        
            Assert.NotNull(result);
            Assert.NotNull(transactionHistoryResponse.transactionHistoryResponse);
        }



    }
}
