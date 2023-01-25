using System.Collections;
using System.Net.Mime;
using AutoMapper;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebLayer.Models;

namespace WebLayer.Controllers;

public class AccountController : Controller
{
    private readonly IService _service;
    
    public AccountController(IService service)
    {
        _service = service;
    }
    // GET
    public IActionResult Index(int id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Account account = _service.GetAccountById(id);
        if (account == null)
        {
            return NotFound();
        }
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Account, AccountViewModel>();
        });
        AccountViewModel accountViewModel = new Mapper(config).Map<Account, AccountViewModel>(account);

        IEnumerable<SelectListItem> AccountList = _service.GetOtherAccounts(account).Select(
            a => new SelectListItem(a.Name, a.Id.ToString()));
        ViewBag.AccountList = AccountList; 
        return View(accountViewModel);
    }

    public IActionResult Expenses(int id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Account account = _service.GetAccountById(id);
        if (account == null)
        {
            return NotFound();
        }
        
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Transaction, TransactionViewModel>();
            cfg.CreateMap<Account, AccountViewModel>();
        });
        IEnumerable<TransactionViewModel> expenses =
            new Mapper(config).Map<IEnumerable<Transaction>, IEnumerable<TransactionViewModel>>(_service.GetExpenses(account));
        return View(expenses);
    }

    public IActionResult Incomes(int id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Account account = _service.GetAccountById(id);
        if (account == null)
        {
            return NotFound();
        }
        
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Transaction, TransactionViewModel>();
            cfg.CreateMap<Account, AccountViewModel>();
        });
        IEnumerable<TransactionViewModel> incomes =
            new Mapper(config).Map<IEnumerable<Transaction>, IEnumerable<TransactionViewModel>>(_service.GetIncomes(account));
        return View(incomes);
    }

    public IActionResult Categories(int id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Account account = _service.GetAccountById(id);
        if (account == null)
        {
            return NotFound();
        }
        Dictionary<string, decimal> categories = _service.GetCategories(account);
        return View(categories);
    }
   
}