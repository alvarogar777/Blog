using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Rotativa;


namespace blog.Controllers
{
    public class CategoriaController : Controller
    {
        // GET: Categoria
        public async Task<ActionResult> Index()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://localhost:5001/api/v1/Categorias");
            //Descargar Tools->NugetPackageManager->Package Manager Console -> Install-Package Newtonsoft.Json 
            var listaCategoria = JsonConvert.DeserializeObject<List<Categoria>>(json);
            return View(listaCategoria);
        }

        public ActionResult FormCreate()
        {
            return View();
        }

        public async Task<ActionResult> ListCategoria()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer "+ "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InVzdWFyaW8xQGhvdG1haWwuY29tIiwiQ3VhbHFpZXJWYWxvciI6IlZhbG9yIGRlIGxhIGxsYXZlIiwianRpIjoiZGNjNmM5NjMtZGZkZi00ZWU3LWEzNTctOWM0ZTQ4NDg2ZjYyIiwiZXhwIjoxNTY3NjUxMzMyfQ.twHlG4mp2zMAL0DV4XPuLOMIAbgxPj1UOSQKLoobEks");
            var json = await httpClient.GetStringAsync("https://localhost:44349/api/v1/Categorias");
            var listaCategoria = JsonConvert.DeserializeObject<List<Categoria>>(json);
            return View(listaCategoria);
        }
 
        public async Task<ActionResult> Create(Categoria categoria)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349/api/v1/");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InVzdWFyaW8xQGhvdG1haWwuY29tIiwiQ3VhbHFpZXJWYWxvciI6IlZhbG9yIGRlIGxhIGxsYXZlIiwianRpIjoiZGNjNmM5NjMtZGZkZi00ZWU3LWEzNTctOWM0ZTQ4NDg2ZjYyIiwiZXhwIjoxNTY3NjUxMzMyfQ.twHlG4mp2zMAL0DV4XPuLOMIAbgxPj1UOSQKLoobEks");
                //HTTP POST
                var postTask = client.PostAsJsonAsync<Categoria>("categorias", categoria);
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

        
        public async Task<ActionResult> Edit(string id)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://localhost:44349/api/v1/categorias"+"/"+id);
            var listaCategoria = JsonConvert.DeserializeObject<Categoria>(json);
            return View(listaCategoria);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            var httpClient = new HttpClient();

            var json = await httpClient.DeleteAsync("https://localhost:44349/api/v1/categorias" + "/" + id);

            
                return RedirectToAction(" ListCategoria");

        }

        public ActionResult Print()
        {
            return new ActionAsPdf("Index", new { nombre = "Pedro" }){FileName ="Test.pdf" };
        }


    }

    public class Categoria{
        public string codigoCategoria { get; set; }
        public string descripcion { get; set; }
   
    }
}