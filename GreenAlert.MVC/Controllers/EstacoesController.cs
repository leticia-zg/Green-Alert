using GreenAlert.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace GreenAlert.MVC.Controllers
{
    public class EstacoesController : Controller
    {
        private readonly HttpClient _http;

        public EstacoesController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("GreenAlertAPI");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var estacoes = await _http.GetFromJsonAsync<List<EstacaoViewModel>>("api/Estacoes");
            return View(estacoes);
        }
    }
}
