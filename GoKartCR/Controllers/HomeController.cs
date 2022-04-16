using GoKartCR.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GoKartCR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Iniciar session
        public IActionResult IniciarSession(string usuario, string password)
        {
            try{
                using(var http = new HttpClient()){
                    var response = http.GetAsync("http://localhost:5000/api/Usuarios/IniciarSession?usuario="+usuario+"&password="+password).Result;
                    if(response.IsSuccessStatusCode){
                        var usuarioJson = response.Content.ReadAsStringAsync().Result;
                        var usuarioObj = JsonConvert.DeserializeObject<Usuario>(usuarioJson);
                        if(usuarioObj != null){
                            HttpContext.Session.SetString("usuario", usuarioObj.Nombre);
                            HttpContext.Session.SetString("idUsuario", usuarioObj.Id.ToString());
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}