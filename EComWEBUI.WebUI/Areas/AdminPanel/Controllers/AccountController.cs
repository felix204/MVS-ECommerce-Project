using EComWEBUI.Core.DTO.Login;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Text.Json.Serialization;

namespace EComWEBUI.WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AccountController : Controller
    {
        [HttpGet("/Admin/Login")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("/AdminLogin")]
        public async Task<IActionResult> AdminLogin(LoginDTO loginDTO)
        {
            var url = "http://localhost:7135/login";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(loginDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            var responseObject = JsonConvert.DeserializeObject<LoginDTO>(response.Content);

            if (string.IsNullOrEmpty(responseObject.Token))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["LoginError"] = "Kullanıcı Adı Yanlış";
            return View("Index");
        }
    }
}
