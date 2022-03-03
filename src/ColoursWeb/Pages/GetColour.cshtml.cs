using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ColoursWeb.Pages
{
    public class ColoursItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }

    }


    public class GetColourModel : PageModel
    {
        public string strResponse;
        public int iStatusCode = 200;

        private readonly HttpClient _httpClient;

        public GetColourModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public static async Task<string> OnGetPink()
        {
            string[] strColors = { "pink", "hotpink", "deeppink", "fuchsia", "mediumvioletred" };
            Random r = new Random();
            int rInt = r.Next(strColors.Length);

            ColoursItem _Item = new ColoursItem()
            {
                Id = 1,
                Name = strColors[rInt],
                Data = ""
            };

            await Task.Run(() => { });

            return JsonSerializer.Serialize(_Item, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true });

        }

        public async Task<string> OnGetRelay(string vURL)
        {

            try
            {
                var msg = await _httpClient.GetStringAsync(vURL);

                return msg;
            }
            catch (Exception ex)
            {
                ProblemDetails res = new ProblemDetails { Status = 500, Title = ex.Message };

                return res.ToString();
            }

        }

        public async Task OnGetAsync()
        {
            var vURL = Request.Cookies["APIUrl"];

            if (vURL == "pink")
            {
                strResponse = await OnGetPink();
            }
            else
            {
                try
                {
                    strResponse = await _httpClient.GetStringAsync(vURL);

                }
                catch (Exception ex)
                {
                    iStatusCode = 500;
                    ProblemDetails res = new ProblemDetails { Status = 500, Title = ex.Message };
                    strResponse = JsonSerializer.Serialize(res, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true });
                }
            }
        }
    }

}
