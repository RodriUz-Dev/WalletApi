using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using WalletApi.Application.Services;
using WalletApi.Domain.Interfaces;
using WalletApi.Infrastructure.Persistence;
using WalletApi.Infrastructure.Repositories;
using WalletApi.Infrastructure.UnitOfWork;

namespace WalletApi.ServiceExtension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ConnectionDB"));
            });

            
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<ITransactionHistoryRepository, TransactionHistoryRepository>();            
            services.AddScoped<ITransactionHistoryService, TransactionHistoryService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

    }
}
