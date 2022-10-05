using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CIS341_lab4.Pages
{
    // shamelessly copied from the snippet
    // https://github.com/dotnet/AspNetCore.Docs/blob/9cdc4687d0ce17bae00cd47b9439edac0bd63bb2/aspnetcore/fundamentals/error-handling/samples/6.x/ErrorHandlingSample/Pages/StatusCode.cshtml.cs
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class StatusCodeModel : PageModel
    {
        public int OriginalStatusCode { get; set; }

        public string? OriginalPathAndQuery { get; set; }

        public void OnGet(int statusCode)
        {
            OriginalStatusCode = statusCode;

            var statusCodeReExecuteFeature =
                HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            if (statusCodeReExecuteFeature is not null)
            {
                OriginalPathAndQuery = string.Join(
                    statusCodeReExecuteFeature.OriginalPathBase,
                    statusCodeReExecuteFeature.OriginalPath,
                    statusCodeReExecuteFeature.OriginalQueryString);
            }

            if (statusCode == 404)
            {
                ViewData["Message"] =
                    "A message that communicates to the user that the requested page could not be found.";
            }
            else
            {
                ViewData["Message"] = $"This is a status code {statusCode} page.";
            }
        }
    }
}