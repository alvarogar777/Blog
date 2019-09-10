using blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace blog.Controllers
{
    public class PruebaDataBaseController : Controller
    {

        //
        // GET: PruebaDataBase
        public ActionResult Index()
        {
            //Database
            DataBase db = new DataBase();

            var listaUusarios = db.Usuario.ToList();
            
            db.Usuario.Find(1);
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            Data = listaUusarios
            };
        }
    }
}