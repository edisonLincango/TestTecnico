using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TestClient.Models;

namespace TestClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Modelo modelo = new Modelo();
            ObtenerColas(modelo);
            return View(modelo);            
        }

        public void ObtenerColas(Modelo modelo) {

            List<Cola> colas = GetColas();
            modelo.Cola1 = colas.FirstOrDefault(x => x.numeroCola == 1);
            modelo.Cola2 = colas.FirstOrDefault(x => x.numeroCola == 2);
            ObtenerClientes(modelo);

        }

        public void ObtenerClientes(Modelo modelo) {
            List<Cliente> clientes = GetClientes();

            modelo.Clientes1 = clientes.Where(x => x.numeroCola == modelo.Cola1.numeroCola).ToList();
            modelo.Clientes2 = clientes.Where(x => x.numeroCola == modelo.Cola2.numeroCola).ToList();

        } 
        public List<Cola> GetColas()
        {
            IEnumerable<Cola> colas = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44363/api/cola");

                var responseTask = client.GetAsync("Cola");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Cola>>();
                    readTask.Wait();

                    colas = readTask.Result;
                }
                else
                {                    
                    colas = Enumerable.Empty<Cola>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return colas.ToList();
        }

        public List<Cliente> GetClientes()
        {
            IEnumerable<Cliente> clientes = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44363/api/cliente");

                var responseTask = client.GetAsync("Cliente");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Cliente>>();
                    readTask.Wait();

                    clientes = readTask.Result;
                }
                else
                {
                    clientes = Enumerable.Empty<Cliente>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return clientes.ToList();
        }

        [HttpPost]
        public ActionResult Create(Modelo modelo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44363/api/cliente");
                
                var postTask = client.PostAsJsonAsync<Cliente>("cliente", modelo.cliente);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return RedirectToAction("Index");            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}