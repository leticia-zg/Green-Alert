using GreenAlert.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace GreenAlert.MVC.Controllers
{
    public class AlertasController : Controller
    {
        private readonly HttpClient _http;

        public AlertasController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("GreenAlertAPI");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var alertas = await _http.GetFromJsonAsync<List<AlertaViewModel>>("api/Alertas");
            return View(alertas);
        }
    }
}
