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
        Console.WriteLine("/ requested");

        // not the best way to go about this, but works for testing
        try
        {
            tdg.generate(_context);
            Console.WriteLine("database generated");
        }
        catch (Exception e)
        {
            // probably because we already have the data, but
            // also could be some other error so we print it out
            // blanket handling exceptions like this is bad practice
            Console.WriteLine(e.StackTrace);
            Console.WriteLine("This is fine ;)");
        }

        return Redirect("/Tags"); // I could use RedirectPermanent, but Redirect works better for testing
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}