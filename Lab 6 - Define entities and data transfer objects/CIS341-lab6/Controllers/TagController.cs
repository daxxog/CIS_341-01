using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIS341_lab6.Data;
using CIS341_lab6.Data.Entities;
using CIS341_lab6.Models;

namespace CIS341_lab6.Controllers
{
    public class TagController : Controller
    {
        private readonly SqliteContext _context;

        public TagController(SqliteContext context)
        {
            _context = context;
        }

        // GET: Tags
        [Route("/Tags")]
        public async Task<IActionResult> Index()
        {
            if (_context.TaggedInformationItems == null)
            {
                return Problem("Entity set 'SqliteContext.TaggedInformationItems'  is null.");
            }

            IEnumerable<TaggedInformationItem> taggedInformationItems =
                await _context.TaggedInformationItems.ToListAsync();
            Dictionary<String, WeightedTagModel> weightedTags = new Dictionary<String, WeightedTagModel>();

            // count the tags
            foreach (TaggedInformationItem taggedInformationItem in taggedInformationItems)
            {
                string tagName = taggedInformationItem.TagName;
                WeightedTagModel weightedTag;
                if (!weightedTags.TryGetValue(tagName, out weightedTag))
                {
                    weightedTag = new WeightedTagModel
                    {
                        TagName = tagName,
                        Weight = 0,
                    };
                    weightedTags.Add(tagName, weightedTag);
                }

                weightedTag.Weight++;
            }

            // sort tags by name and invert the weights (so they map to h tags used in the view)
            IEnumerable<WeightedTagModel> tags = weightedTags.Values.OrderBy(weightedTag =>
            {
                // sort by tag name
                return weightedTag.TagName;
            }).Select(weightedTag =>
            {
                // smaller number is better weight (because these map to h tags)
                weightedTag.Weight = 7 - weightedTag.Weight;

                // there is a better way to implement this with larger datasets
                // we also could do a lot of this in SQL, but to quote Professor Heimonen:
                //
                // "This isn't a database class."
                //
                if (weightedTag.Weight < 1)
                {
                    weightedTag.Weight = 1;
                }

                return weightedTag;
            }).AsEnumerable();

            return View(tags);
        }

        // GET: Tag/Details/5
        public async Task<IActionResult> Details(string id)
        {
            return View();
        }
    }
}