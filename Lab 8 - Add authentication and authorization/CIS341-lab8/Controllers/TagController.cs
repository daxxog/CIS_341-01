using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIS341_lab8.Data;
using CIS341_lab8.Data.Entities;
using CIS341_lab8.Models;

namespace CIS341_lab8.Controllers
{
    [Authorize]
    public class TagController : Controller
    {
        private readonly SqliteContext _context;
        private readonly Func<AuthorizationStatus> _getAuthorizationStatus;

        public TagController(SqliteContext context)
        {
            _context = context;
            _getAuthorizationStatus = () => (AuthorizationStatus)HttpContext.Items["AuthorizationStatus"];
        }

        // GET: Tags
        [Route("/Tags")]
        [AllowAnonymous]
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

        // GET: Tag/turducken
        [Route("/Tag/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            AuthorizationStatus authStatus = _getAuthorizationStatus();

            if (id == null || _context.TaggedInformationItems == null)
            {
                return NotFound();
            }

            // select by tag name and include all the linked models we need
            IEnumerable<TaggedInformationItem> taggedInformationItems = await _context.TaggedInformationItems
                .Where(m => m.TagName == id).Include(m => m.InformationItemSharedInformationItem)
                .Include(m => m.InformationItemSharedInformationItem.InformationItemFavorites).ToListAsync();
            List<SharedInformationItem> sharedInformationItems = new List<SharedInformationItem>();
            TagModel tagged = null;
            List<MyFavoriteItemModel> maybeMyFavoriteItems = new List<MyFavoriteItemModel>();

            // iterate through the database to create the ViewModel we want to return to the View (tagged)
            foreach (TaggedInformationItem taggedInformationItem in taggedInformationItems)
            {
                // create the TagModel if we haven't already (first iteration)
                if (tagged == null)
                {
                    tagged = new TagModel
                    {
                        TagName = taggedInformationItem.TagName,
                        MaybeMyFavoriteItems = maybeMyFavoriteItems,
                    };
                }

                SharedInformationItem sii = taggedInformationItem.InformationItemSharedInformationItem;
                // we use this MyFavoriteItemModel to send favorite information to the View
                MyFavoriteItemModel maybeMyFavoriteItem = new MyFavoriteItemModel
                {
                    // see if the favorite matches the user id somewhere
                    // this is not optimal for lots of users / favorites
                    // we also could do a lot of this in SQL, but to quote Professor Heimonen:
                    //
                    // "This isn't a database class."
                    //
                    MyFavorite = sii.InformationItemFavorites.Aggregate(false,
                        (a, favorite) => a || favorite.UserId == authStatus.UserId),
                    SharedInformationItem = sii,
                };
                maybeMyFavoriteItems.Add(maybeMyFavoriteItem);
            }


            return View(tagged);
        }
    }
}