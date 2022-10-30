using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CIS341_lab6.Models;

namespace CIS341_lab6.Controllers
{
    public class TagController : Controller
    {
        public TagController()
        {
        }

        // GET: Tags
        [Route("/Tags")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Tag/Details/5
        public async Task<IActionResult> Details(string id)
        {
            return View();
        }
    }
}