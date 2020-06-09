using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoParcial3.Models;

namespace ProyectoParcial3.Controllers
{
    public class PreguntaExamenController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PreguntaExamen
        public ActionResult Index()
        {
            var preguntaExamen = db.PreguntaExamen.Include(p => p.Examen).Include(p => p.Pregunta);
            return View(preguntaExamen.ToList());
        }

        // GET: PreguntaExamen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreguntaExamen preguntaExamen = db.PreguntaExamen.Find(id);
            if (preguntaExamen == null)
            {
                return HttpNotFound();
            }
            return View(preguntaExamen);
        }

        // GET: PreguntaExamen/Create
        public ActionResult Create()
        {
            ViewBag.ExamenID = new SelectList(db.Examen, "ID", "Nombre");
            ViewBag.PreguntaID = new SelectList(db.Preguntas, "ID", "Nombre");
            return View();
        }

        // POST: PreguntaExamen/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ExamenID,PreguntaID")] PreguntaExamen preguntaExamen)
        {
            if (ModelState.IsValid)
            {
                db.PreguntaExamen.Add(preguntaExamen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExamenID = new SelectList(db.Examen, "ID", "Nombre", preguntaExamen.ExamenID);
            ViewBag.PreguntaID = new SelectList(db.Preguntas, "ID", "Nombre", preguntaExamen.PreguntaID);
            return View(preguntaExamen);
        }

        // GET: PreguntaExamen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreguntaExamen preguntaExamen = db.PreguntaExamen.Find(id);
            if (preguntaExamen == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExamenID = new SelectList(db.Examen, "ID", "Nombre", preguntaExamen.ExamenID);
            ViewBag.PreguntaID = new SelectList(db.Preguntas, "ID", "Nombre", preguntaExamen.PreguntaID);
            return View(preguntaExamen);
        }

        // POST: PreguntaExamen/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ExamenID,PreguntaID")] PreguntaExamen preguntaExamen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(preguntaExamen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExamenID = new SelectList(db.Examen, "ID", "Nombre", preguntaExamen.ExamenID);
            ViewBag.PreguntaID = new SelectList(db.Preguntas, "ID", "Nombre", preguntaExamen.PreguntaID);
            return View(preguntaExamen);
        }

        // GET: PreguntaExamen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreguntaExamen preguntaExamen = db.PreguntaExamen.Find(id);
            if (preguntaExamen == null)
            {
                return HttpNotFound();
            }
            return View(preguntaExamen);
        }

        // POST: PreguntaExamen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PreguntaExamen preguntaExamen = db.PreguntaExamen.Find(id);
            db.PreguntaExamen.Remove(preguntaExamen);
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
