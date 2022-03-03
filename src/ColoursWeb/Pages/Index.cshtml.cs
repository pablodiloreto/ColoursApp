using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ColourWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public string Api { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var api = Request.Cookies.FirstOrDefault(x => x.Key == "APIUrl");

            if (api.Value is not null)
            {
                Api = api.Value;  
            }
            else
            {
                Api = "https://coloursapi.app.aprender.it/colours/random";
            }
        }
    }
}
