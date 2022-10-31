using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CIS341_lab6.Models;
using CIS341_lab6.Data;

namespace CIS341_lab6.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SqliteContext _context;

    public HomeController(ILogger<HomeController> logger, SqliteContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index(TestDataGenerator tdg)
    {
        tdg.generate(_context);
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}