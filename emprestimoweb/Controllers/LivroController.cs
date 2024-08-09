using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using emprestimoweb.Models;

namespace emprestimoweb.Controllers
{
    public class LivroController : Controller
    {
        private bibliotecaEntities db = new bibliotecaEntities();

        // GET: Livro
        public ActionResult Index()
        {
            var livro = db.Livro.Include(l => l.Autor1).Include(l => l.Editora1);
            return View(livro.ToList());
        }

        // GET: Livro/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livro.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        // GET: Livro/Create
        public ActionResult Create()
        {
            ViewBag.Autor = new SelectList(db.Autor, "Codigo", "Nome");
            ViewBag.Editora = new SelectList(db.Editora, "Codigo", "Nome");
            return View();
        }

        // POST: Livro/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Autor,Editora,ISBN,Titulo")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                db.Livro.Add(livro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Autor = new SelectList(db.Autor, "Codigo", "Nome", livro.Autor);
            ViewBag.Editora = new SelectList(db.Editora, "Codigo", "Nome", livro.Editora);
            return View(livro);
        }

        // GET: Livro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livro.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            ViewBag.Autor = new SelectList(db.Autor, "Codigo", "Nome", livro.Autor);
            ViewBag.Editora = new SelectList(db.Editora, "Codigo", "Nome", livro.Editora);
            return View(livro);
        }

        // POST: Livro/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Autor,Editora,ISBN,Titulo")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(livro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Autor = new SelectList(db.Autor, "Codigo", "Nome", livro.Autor);
            ViewBag.Editora = new SelectList(db.Editora, "Codigo", "Nome", livro.Editora);
            return View(livro);
        }

        // GET: Livro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livro.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        // POST: Livro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Livro livro = db.Livro.Find(id);
            db.Livro.Remove(livro);
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
