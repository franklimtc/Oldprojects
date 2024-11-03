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
    public class CadastroSetorsController : Controller
    {
        private ModeldnaPrint db = new ModeldnaPrint();

        // GET: CadastroSetors
        public ActionResult Index()
        {
            var cadastroSetor = db.CadastroSetor.Include(c => c.CadastroUnidade);
            return View(cadastroSetor.Where(x => x.status == "1").ToList());
        }

        // GET: CadastroSetors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CadastroSetor cadastroSetor = db.CadastroSetor.Find(id);
            if (cadastroSetor == null)
            {
                return HttpNotFound();
            }
            return View(cadastroSetor);
        }

        // GET: CadastroSetors/Create
        public ActionResult Create()
        {
            ViewBag.idLocalidade = new SelectList(db.CadastroUnidade, "idLocalidade", "descricao");
            return View();
        }

        // POST: CadastroSetors/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idSetor,idLocalidade,descricao,centroCusto,status,cotaMensal")] CadastroSetor cadastroSetor)
        {
            if (ModelState.IsValid)
            {
                db.CadastroSetor.Add(cadastroSetor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idLocalidade = new SelectList(db.CadastroUnidade, "idLocalidade", "descricao", cadastroSetor.idLocalidade);
            return View(cadastroSetor);
        }

        // GET: CadastroSetors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CadastroSetor cadastroSetor = db.CadastroSetor.Find(id);
            if (cadastroSetor == null)
            {
                return HttpNotFound();
            }
            ViewBag.idLocalidade = new SelectList(db.CadastroUnidade, "idLocalidade", "descricao", cadastroSetor.idLocalidade);
            return View(cadastroSetor);
        }

        // POST: CadastroSetors/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idSetor,idLocalidade,descricao,centroCusto,status,cotaMensal")] CadastroSetor cadastroSetor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cadastroSetor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idLocalidade = new SelectList(db.CadastroUnidade, "idLocalidade", "descricao", cadastroSetor.idLocalidade);
            return View(cadastroSetor);
        }

        // GET: CadastroSetors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CadastroSetor cadastroSetor = db.CadastroSetor.Find(id);
            if (cadastroSetor == null)
            {
                return HttpNotFound();
            }
            return View(cadastroSetor);
        }

        // POST: CadastroSetors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CadastroSetor cadastroSetor = db.CadastroSetor.Find(id);
            cadastroSetor.status = "0";
            db.Entry(cadastroSetor).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
