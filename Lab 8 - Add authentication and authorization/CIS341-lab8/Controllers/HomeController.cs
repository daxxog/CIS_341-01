using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CIS341_lab8.Models;
using CIS341_lab8.Data;

namespace CIS341_lab8.Controllers;

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
        return Redirect("/Tags"); // I could use RedirectPermanent, but Redirect works better for testing
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}