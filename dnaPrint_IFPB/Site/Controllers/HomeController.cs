using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Site.Models;


namespace Site.Controllers
{
    public class HomeController : Controller
    {
        private ModeldnaPrint db = new ModeldnaPrint();

        public ActionResult Index()
        {
            ViewBag.Title = "Dashboard";
            ViewBag.Disponibilidade = $"{db.vw_disponibilidade3.Where(x => x.qtdDias == 0).Count()}/{db.vw_disponibilidade3.Count()}";
            ViewBag.Suprimentos = $"{db.vw_suprimentos2.Where( x=> x.Toner < 20).Count()}";
            ViewBag.Volume = $"{db.vw_bilhetagemAtual.First().VolumeTotal}";
            try
            {
                ViewBag.Falhas = $"{db.vw_ErrosEquipamentos.Sum(x => x.Erros).Value}";
            }
            catch 
            {
                ViewBag.Falhas = $"0";
            }

            var tempBilhetagemSemanal = db.tempBilhetagemSemanal;
            //var tempBilhetagemSemanal = db.CadastroEquipamentos.Include(c => c.)

            return View(tempBilhetagemSemanal.ToList());
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