using GreenAlert.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace GreenAlert.MVC.Controllers
{
    public class SensoresController : Controller
    {
        private readonly HttpClient _http;

        public SensoresController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("GreenAlertAPI");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var sensores = await _http.GetFromJsonAsync<List<SensorViewModel>>("api/Sensores");
            return View(sensores);
        }
    }
}
