using System.Drawing;
using Microsoft.Extensions.DependencyInjection;

using DataAccessLayer.Repositories;
using BusinessLogicLayer.Services;
using PresentationLayer;
using DataAccessLayer.DB;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;



    using IHost host = Host.CreateDefaultBuilder(args).ConfigureServices(services =>
    {
        services.AddScoped<IRepository<Account>, AccountRepository>();
        services.AddScoped<IRepository<Transaction>, TransactionRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IService, Service>();
        services.AddScoped<IConsoleMenu, ConsoleMenu>();
        services.AddDbContext<WalletContext>(options =>
        options.UseLazyLoadingProxies()
            .UseSqlServer(
                "Server=localhost;Database=WalletDB;Trusted_Connection=True;TrustServerCertificate=True"));
    }).Build();
    
    
    host.Services.GetService<IConsoleMenu>().Start();

    

