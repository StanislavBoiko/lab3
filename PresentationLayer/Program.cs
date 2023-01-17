using Microsoft.Extensions.DependencyInjection;

using DataAccessLayer.Repositories;
using BusinessLogicLayer.Services;
using PresentationLayer;
using DataAccessLayer.DB;
using Microsoft.EntityFrameworkCore;

static void Main(string[] args)
{

    var services = new ServiceCollection();
    services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<IAccountService, AccountService>();
    services.AddScoped<ITransactionService, ITransactionService>();
    services.AddScoped<IService, Service>();
    services.AddScoped<IConsoleMenu, ConsoleMenu>();
    services.AddDbContext<WalletContext>(options => options.UseSqlServer("Server=localhost;Database=WalletDB;Trusted_Connection=True;TrustServerCertificate=True"));
    var serviceProvider = services.BuildServiceProvider();

    IService service = serviceProvider.GetService<Service>();
    IConsoleMenu menu = new ConsoleMenu(service);
    menu.Start();
}