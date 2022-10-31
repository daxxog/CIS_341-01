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
            IEnumerable<WeightedTagModel> tags = new WeightedTagModel[]
            {
                new WeightedTagModel
                {
                    TagName = "sausage",
                    Weight = 5,
                },
                new WeightedTagModel
                {
                    TagName = "pork shoulder",
                    Weight = 2,
                },
                new WeightedTagModel
                {
                    TagName = "bacon",
                    Weight = 4,
                },
            };
            return View(tags);
        }

        // GET: Tag/Details/5
        public async Task<IActionResult> Details(string id)
        {
            return View();
        }
    }
}