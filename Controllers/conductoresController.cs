using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmpresaTaxis;

namespace EmpresaTaxis.Controllers
{
    public class conductoresController : Controller
    {
        private empresataxisEntities db = new empresataxisEntities();

        // GET: conductores
        public ActionResult Index()
        {
            var conductores = db.conductores.Include(c => c.taxis);
            return View(conductores.ToList());
        }

        // GET: conductores/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            conductores conductores = db.conductores.Find(id);
            if (conductores == null)
            {
                return HttpNotFound();
            }
            return View(conductores);
        }

        // GET: conductores/Create
        public ActionResult Create()
        {
            ViewBag.placa_id = new SelectList(db.taxis, "id", "Modelo");
            return View();
        }

        // POST: conductores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Nombre,Telefono,placa_id")] conductores conductores)
        {
            if (ModelState.IsValid)
            {
                db.conductores.Add(conductores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.placa_id = new SelectList(db.taxis, "id", "Modelo", conductores.placa_id);
            return View(conductores);
        }

        // GET: conductores/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            conductores conductores = db.conductores.Find(id);
            if (conductores == null)
            {
                return HttpNotFound();
            }
            ViewBag.placa_id = new SelectList(db.taxis, "id", "Modelo", conductores.placa_id);
            return View(conductores);
        }

        // POST: conductores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Nombre,Telefono,placa_id")] conductores conductores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(conductores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.placa_id = new SelectList(db.taxis, "id", "Modelo", conductores.placa_id);
            return View(conductores);
        }

        // GET: conductores/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            conductores conductores = db.conductores.Find(id);
            if (conductores == null)
            {
                return HttpNotFound();
            }
            return View(conductores);
        }

        // POST: conductores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            conductores conductores = db.conductores.Find(id);
            db.conductores.Remove(conductores);
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
