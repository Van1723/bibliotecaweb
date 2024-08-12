using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using emprestimoweb.Models;
using emprestimoweb.Repositorio;

namespace emprestimoweb.Controllers
{
    public class EmprestimoController : Controller
    {
        private bibliotecaEntities db = new bibliotecaEntities();

        // GET: Emprestimo
        public ActionResult Index()
        {
            var emprestimo = db.Emprestimo.Include(e => e.Aluno1);
            return View(emprestimo.ToList());
        }
        public JsonResult GerarNovoEmprestimo()
        {
            EmprestimoRepositorio objGerar = new EmprestimoRepositorio();
            int Id = objGerar.GeraNovoEmprestimo();
            return Json(Id, JsonRequestBehavior.AllowGet);
        }

        public  void InserirItem(ItensEmprestimo objdados)
        {
            EmprestimoItemRepositorio objgravar = new EmprestimoItemRepositorio();
            objgravar.InserirItem(objdados);
        }

        public JsonResult FechaEmprestimo(Emprestimo objdados)
        {
            EmprestimoRepositorio objfechar = new EmprestimoRepositorio();
            objfechar.FecharEmprestimo(objdados);
            return Json(data:"Venda realizada com sucesso!",JsonRequestBehavior.AllowGet); 

        }

        // GET: Emprestimo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = db.Emprestimo.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }
        //pagina de lançamento//
        public ActionResult Lancar()
        {
            // passsar as infos
            ViewBag.Aluno = new SelectList(db.Aluno, "Id", "Nome");
            ViewBag.Livro = new SelectList(db.Livro, "Codigo", "Titulo");

            return View();
        }

        // GET: Emprestimo/Create
        public ActionResult Create()
        {
            ViewBag.Aluno = new SelectList(db.Aluno, "Id", "Nome");
            return View();
        }

        // POST: Emprestimo/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Aluno,Data_Emprestimo,Data_Devolucao")] Emprestimo emprestimo)
        {
            if (ModelState.IsValid)
            {
                db.Emprestimo.Add(emprestimo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Aluno = new SelectList(db.Aluno, "Id", "Nome", emprestimo.Aluno);
            return View(emprestimo);
        }

        // GET: Emprestimo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = db.Emprestimo.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Aluno = new SelectList(db.Aluno, "Id", "Nome", emprestimo.Aluno);
            return View(emprestimo);
        }

        // POST: Emprestimo/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Aluno,Data_Emprestimo,Data_Devolucao")] Emprestimo emprestimo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emprestimo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Aluno = new SelectList(db.Aluno, "Id", "Nome", emprestimo.Aluno);
            return View(emprestimo);
        }

        // GET: Emprestimo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = db.Emprestimo.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }

        // POST: Emprestimo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Emprestimo emprestimo = db.Emprestimo.Find(id);
            db.Emprestimo.Remove(emprestimo);
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
