using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DuHocSeoulWebsite.Models;
using DuHocSeoulWebsite.Utils;
using System.Net;

namespace DuHocSeoulWebsite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendRequest([FromBody]RequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { HasError = true, ErrorMessage = "Lỗi: Dữ liệu không đúng. Vui lòng kiểm tra và thử lại." });
            }
            model.Content = $"[DUHOCSEOUL.COM] {model.Content}";
            var statusCode = await PostByHttpClient(CommonConstants.ApiToSendRequestUrl, ToStringContentJson(model));
            if (statusCode != HttpStatusCode.OK)
            {
                return new JsonResult(new { HasError = true, ErrorMessage = "Lỗi: Không thể gửi yêu cầu." });
            }

            return new JsonResult(new { HasError = false });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private StringContent ToStringContentJson(object o)
           => new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");

        private async Task<HttpStatusCode> PostByHttpClient(string url, HttpContent httpContent)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, xcert, chain, errors) =>
                {
                    return true;
                }
            };
            using (var webClient = new HttpClient(handler))
            {
                HttpContent contentJson = httpContent;
                var result = await webClient.PostAsync(url, contentJson);
                return result.StatusCode;
            }
        }
    }
}
