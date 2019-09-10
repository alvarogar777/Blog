using blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace blog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult MyPage()
        {
            ViewBag.Message = "Titulo de la pagina";
            ViewBag.Title = "Titulo";
            return View();
        }
        public ActionResult Index(string mesagge = "")
        {
            ViewBag.Message =mesagge;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password, object sender, AuthenticateEventArgs e)
        {
            
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                DataBase db = new DataBase();
                var user = db.Usuario.FirstOrDefault(i => i.Email == email && i.Password == password);
                if(user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Email,true);
              
                    //Session["user"] = user.Email;
                    //FormsAuthentication.RedirectFromLoginPage(user.Email, true);
                    return RedirectToAction("Index", "Profile");                    
                }
                else
                {
                    return RedirectToAction("Index", new { mesagge = "No se encontro usuario" });
                }
            }
            else
            {
                return RedirectToAction("Index",new { mesagge = "No se encontro datos" });
            }
            
           
        } 

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult About(string email)
        {
            ViewBag.Message = email;
            //Modelo = Usuario

            List<Usuario2> usuarios = new List<Usuario2>()
            {
                new Usuario2 { Email=email,Nombre="Daniel" },
                 new Usuario2 { Email="MyEmail",Nombre="Carlos" },
            };

       


            return View(usuarios);
        }
        
        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
               
    }

    public class Usuario2
    {
        public string Email { get; set; }
        public string Nombre { get; set; }

        public void SendEmail()
        {

        }
    }
 
}