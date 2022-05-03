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
    public class taxisController : Controller
    {
        private empresataxisEntities db = new empresataxisEntities();

        // GET: taxis
        public ActionResult Index()
        {
            var taxis = db.taxis.Include(t => t.propietarios);
            return View(taxis.ToList());
        }

        // GET: taxis/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            taxis taxis = db.taxis.Find(id);
            if (taxis == null)
            {
                return HttpNotFound();
            }
            return View(taxis);
        }

        // GET: taxis/Create
        public ActionResult Create()
        {
            ViewBag.propietarios_id = new SelectList(db.propietarios, "id", "Nombre");
            return View();
        }

        // POST: taxis/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Modelo,Marca,Telefono,propietarios_id")] taxis taxis)
        {
            if (ModelState.IsValid)
            {
                db.taxis.Add(taxis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.propietarios_id = new SelectList(db.propietarios, "id", "Nombre", taxis.propietarios_id);
            return View(taxis);
        }

        // GET: taxis/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            taxis taxis = db.taxis.Find(id);
            if (taxis == null)
            {
                return HttpNotFound();
            }
            ViewBag.propietarios_id = new SelectList(db.propietarios, "id", "Nombre", taxis.propietarios_id);
            return View(taxis);
        }

        // POST: taxis/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Modelo,Marca,Telefono,propietarios_id")] taxis taxis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taxis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.propietarios_id = new SelectList(db.propietarios, "id", "Nombre", taxis.propietarios_id);
            return View(taxis);
        }

        // GET: taxis/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            taxis taxis = db.taxis.Find(id);
            if (taxis == null)
            {
                return HttpNotFound();
            }
            return View(taxis);
        }

        // POST: taxis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            taxis taxis = db.taxis.Find(id);
            db.taxis.Remove(taxis);
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
