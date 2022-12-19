using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CIS341_checkpoint3.Models;
using CIS341_checkpoint3.Data;

namespace CIS341_checkpoint3.Controllers
{
    /// <summary>
    /// Controller class for the Index of the application.
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SqliteContext _context;

        public HomeController(ILogger<HomeController> logger, SqliteContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: /
        /// <summary>
        /// The Index of the application simply redirects to the Tag controller Index.
        /// </summary>
        /// <returns>
        /// A redirection to the Tag controller Index.
        /// </returns>
        [AllowAnonymous]
        public IActionResult Index()
        {
            return Redirect("/Tags"); // I could use RedirectPermanent, but Redirect works better for testing
        }

        /// <summary>
        /// Error handling.
        /// </summary>
        /// <returns>
        /// A view containing the error and trace. This shouldn't be used in production.
        /// </returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}