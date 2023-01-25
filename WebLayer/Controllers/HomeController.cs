using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebLayer.Models;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebLayer.Controllers;

public class HomeController : Controller
{
    private readonly IService _service;

    public HomeController(IService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Account, AccountViewModel>();
        });
        var accounts =
            new Mapper(config).Map<IEnumerable<Account>, IEnumerable<AccountViewModel>>(_service.GetAllAccounts());

        return View(accounts);
    }

    //GET
    public IActionResult Create()
    {
        return View();
    }
    
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AccountViewModel accountViewModel)
    {
        if (ModelState.IsValid)
        {


            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<AccountViewModel, Account>());
            var account = new Mapper(config).Map<AccountViewModel, Account>(accountViewModel);
            _service.AddAccount(account);
            return RedirectToAction("Index");
        }

        return View(accountViewModel);
    }

    public IActionResult Transfer()
    {
        IEnumerable<SelectListItem> accounts = _service.GetAllAccounts().Select(
            a => new SelectListItem(a.Name, a.Id.ToString()));
        ViewBag.accounts = accounts;
        return View();
    }

    [HttpPost]
    public IActionResult Transfer(TransferViewModel transferViewModel)
    {
        if (transferViewModel.SenderId == transferViewModel.RecipientId)
        {
            ModelState.AddModelError("Transfer Error", "Sender and recipient should be different accounts");
        }

        if (ModelState.IsValid)
        {
            Account sender = _service.GetAccountById(transferViewModel.SenderId);
            Account recipient = _service.GetAccountById(transferViewModel.RecipientId);
            _service.TransferBetweenAccounts(sender, recipient, transferViewModel.Amount);
            return RedirectToAction("Index");
        }
        return View();
    }
    
    
    
    
}