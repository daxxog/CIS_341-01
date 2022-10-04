using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CIS341_lab4.Pages
{
    public class Contact
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }

    public class ContactModel : PageModel
    {
        [BindProperty] public Contact? ContactForm { get; set; }

        private readonly LinkGenerator _linkGenerator;

        public ContactModel(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // https://stackoverflow.com/a/19137100
            Console.WriteLine($"POST data={Newtonsoft.Json.JsonConvert.SerializeObject(ContactForm)}");

            return Redirect(_linkGenerator.GetPathByPage("/About/Thanks") ?? "");
        }
    }
}