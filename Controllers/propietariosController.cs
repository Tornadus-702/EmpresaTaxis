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
    public class propietariosController : Controller
    {
        private empresataxisEntities db = new empresataxisEntities();

        // GET: propietarios
        public ActionResult Index()
        {
            return View(db.propietarios.ToList());
        }

        // GET: propietarios/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            propietarios propietarios = db.propietarios.Find(id);
            if (propietarios == null)
            {
                return HttpNotFound();
            }
            return View(propietarios);
        }

        // GET: propietarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: propietarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Nombre,Apellidos,Telefono")] propietarios propietarios)
        {
            if (ModelState.IsValid)
            {
                db.propietarios.Add(propietarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(propietarios);
        }

        // GET: propietarios/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            propietarios propietarios = db.propietarios.Find(id);
            if (propietarios == null)
            {
                return HttpNotFound();
            }
            return View(propietarios);
        }

        // POST: propietarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Nombre,Apellidos,Telefono")] propietarios propietarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propietarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(propietarios);
        }

        // GET: propietarios/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            propietarios propietarios = db.propietarios.Find(id);
            if (propietarios == null)
            {
                return HttpNotFound();
            }
            return View(propietarios);
        }

        // POST: propietarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            propietarios propietarios = db.propietarios.Find(id);
            db.propietarios.Remove(propietarios);
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
