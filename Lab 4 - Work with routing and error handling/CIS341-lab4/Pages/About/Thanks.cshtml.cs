using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CIS341_lab4.Pages
{
    public class ThanksModel : PageModel
    {
        private readonly LinkGenerator _linkGenerator;

        public ThanksModel(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        public void OnGet()
        {
            string dateTime = DateTime.Now.ToString("r", new CultureInfo("en-US"));
            ViewData["TimeStamp"] = dateTime;
            ViewData["ContactFormPath"] = _linkGenerator.GetPathByPage("/About/Contact");
        }
    }
}