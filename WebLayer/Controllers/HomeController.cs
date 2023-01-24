using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebLayer.Models;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;

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
    
}