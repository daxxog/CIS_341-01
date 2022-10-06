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

        private readonly ILogger _logger;

        public StatusCodeModel(ILogger<StatusCodeModel> logger)
        {
            _logger = logger;
        }

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

            // arguably this could be _logger.LogWarning or some other higher level
            // but to me this just seems informational, as users could be hitting these status code pages all the time,
            // and the information can be filtered down to this class anyways because it is annotated ILogger<StatusCodeModel>
            // ----
            // OriginalPathAndQuery only seems to work with UseStatusCodePagesWithReExecute, so we don't use it here,
            // and I don't think I need it anyways because:
            //
            // the lab says "Include at least the client's user agent information, the requested URL, and the HTTP status code."
            // which doesn't really says "which" requested URL (original, or status code page) so I think we are good here
            var requestedURL = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";
            _logger.LogInformation(
                $"UserAgent={Request.Headers.UserAgent}, requestedURL={requestedURL}, statusCode={statusCode},");
        }
    }
}