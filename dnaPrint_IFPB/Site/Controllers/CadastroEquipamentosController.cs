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
    public class CadastroEquipamentosController : Controller
    {
        private ModeldnaPrint db = new ModeldnaPrint();

        // GET: CadastroEquipamentos
        public ActionResult Index()
        {
            var cadastroEquipamentos = db.CadastroEquipamentos.Include(c => c.CadastroCidade).Include(c => c.CadastroEquipamentoModelo).Include(c => c.CadastroEstado).Include(c => c.CadastroSetor).Include(c => c.CadastroUnidade);
            return View(cadastroEquipamentos.Where(x => x.status == true).ToList());
        }

        // GET: CadastroEquipamentos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CadastroEquipamentos cadastroEquipamentos = db.CadastroEquipamentos.Find(id);
            if (cadastroEquipamentos == null)
            {
                return HttpNotFound();
            }
            return View(cadastroEquipamentos);
        }

        // GET: CadastroEquipamentos/Create
        public ActionResult Create()
        {
            ViewBag.idCidade = new SelectList(db.CadastroCidade, "idCidade", "cidade");
            ViewBag.idModeloEquipamento = new SelectList(db.CadastroEquipamentoModelo, "idModeloEquipamento", "Modelo");
            ViewBag.idEstado = new SelectList(db.CadastroEstado, "idEstado", "UF");
            ViewBag.idSetor = new SelectList(db.CadastroSetor, "idSetor", "descricao");
            ViewBag.idLocalidade = new SelectList(db.CadastroUnidade, "idLocalidade", "descricao");


            return View();
        }

        // POST: CadastroEquipamentos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEquipamento,idEstado,idCidade,idLocalidade,idSetor,idModeloEquipamento,ip,serie,nome,dtAtivacao")] CadastroEquipamentos cadastroEquipamentos)
        {
            cadastroEquipamentos.status = true;
            cadastroEquipamentos.dtModificacao = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.CadastroEquipamentos.Add(cadastroEquipamentos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCidade = new SelectList(db.CadastroCidade, "idCidade", "cidade", cadastroEquipamentos.idCidade);
            ViewBag.idModeloEquipamento = new SelectList(db.CadastroEquipamentoModelo, "idModeloEquipamento", "Fabricante", cadastroEquipamentos.idModeloEquipamento);
            ViewBag.idEstado = new SelectList(db.CadastroEstado, "idEstado", "UF", cadastroEquipamentos.idEstado);
            ViewBag.idSetor = new SelectList(db.CadastroSetor, "idSetor", "descricao", cadastroEquipamentos.idSetor);
            ViewBag.idLocalidade = new SelectList(db.CadastroUnidade, "idLocalidade", "descricao", cadastroEquipamentos.idLocalidade);
            return View(cadastroEquipamentos);
        }

        // GET: CadastroEquipamentos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CadastroEquipamentos cadastroEquipamentos = db.CadastroEquipamentos.Find(id);
            if (cadastroEquipamentos == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCidade = new SelectList(db.CadastroCidade, "idCidade", "cidade", cadastroEquipamentos.idCidade);
            ViewBag.idModeloEquipamento = new SelectList(db.CadastroEquipamentoModelo, "idModeloEquipamento", "Fabricante", cadastroEquipamentos.idModeloEquipamento);
            ViewBag.idEstado = new SelectList(db.CadastroEstado, "idEstado", "UF", cadastroEquipamentos.idEstado);
            ViewBag.idSetor = new SelectList(db.CadastroSetor, "idSetor", "descricao", cadastroEquipamentos.idSetor);
            ViewBag.idLocalidade = new SelectList(db.CadastroUnidade, "idLocalidade", "descricao", cadastroEquipamentos.idLocalidade);
            return View(cadastroEquipamentos);
        }

        // POST: CadastroEquipamentos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEquipamento, ip,nome,status")] CadastroEquipamentos EqpAtual)
        {
            var EqpUpdate = db.CadastroEquipamentos.Find(EqpAtual.idEquipamento);
            EqpUpdate.dtModificacao = DateTime.Now;


            if (EqpUpdate.nome != EqpAtual.nome)
                EqpUpdate.nome = EqpAtual.nome;

            if (EqpUpdate.ip != EqpAtual.ip)
                EqpUpdate.ip = EqpAtual.ip;

            if (EqpUpdate.status != EqpAtual.status)
            {
                EqpUpdate.status = EqpAtual.status;

                if (EqpAtual.status == false)
                    EqpUpdate.dtDesativacao = DateTime.Now;
            }

            if (ModelState.IsValid)
            {
                db.Entry(EqpUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCidade = new SelectList(db.CadastroCidade, "idCidade", "cidade", EqpUpdate.idCidade);
            ViewBag.idModeloEquipamento = new SelectList(db.CadastroEquipamentoModelo, "idModeloEquipamento", "Fabricante", EqpUpdate.idModeloEquipamento);
            ViewBag.idEstado = new SelectList(db.CadastroEstado, "idEstado", "UF", EqpUpdate.idEstado);
            ViewBag.idSetor = new SelectList(db.CadastroSetor, "idSetor", "descricao", EqpUpdate.idSetor);
            ViewBag.idLocalidade = new SelectList(db.CadastroUnidade, "idLocalidade", "descricao", EqpUpdate.idLocalidade);
            return View(EqpUpdate);
        }

        // GET: CadastroEquipamentos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CadastroEquipamentos cadastroEquipamentos = db.CadastroEquipamentos.Find(id);
            if (cadastroEquipamentos == null)
            {
                return HttpNotFound();
            }
            return View(cadastroEquipamentos);
        }

        // POST: CadastroEquipamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CadastroEquipamentos cadastroEquipamentos = db.CadastroEquipamentos.Find(id);
            cadastroEquipamentos.status = false;
            cadastroEquipamentos.dtDesativacao = DateTime.Now;
            //db.CadastroEquipamentos.Remove(cadastroEquipamentos);

            db.Entry(cadastroEquipamentos).State = EntityState.Modified;
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
