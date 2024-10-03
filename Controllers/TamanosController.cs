using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ElCuernoVip.Models;

namespace ElCuernoVip.Controllers
{
    public class TamanosController : Controller
    {
        private VentaCuernoEntities1 db = new VentaCuernoEntities1();

        // GET: Tamanos
        public ActionResult Index()
        {
            return View(db.Tamanos.ToList());
        }

        // GET: Tamanos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tamanos tamanos = db.Tamanos.Find(id);
            if (tamanos == null)
            {
                return HttpNotFound();
            }
            return View(tamanos);
        }

        // GET: Tamanos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tamanos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Tamano,Descripcion")] Tamanos tamanos)
        {
            if (ModelState.IsValid)
            {
                db.Tamanos.Add(tamanos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tamanos);
        }

        // GET: Tamanos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tamanos tamanos = db.Tamanos.Find(id);
            if (tamanos == null)
            {
                return HttpNotFound();
            }
            return View(tamanos);
        }

        // POST: Tamanos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Tamano,Descripcion")] Tamanos tamanos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tamanos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tamanos);
        }

        // GET: Tamanos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tamanos tamanos = db.Tamanos.Find(id);
            if (tamanos == null)
            {
                return HttpNotFound();
            }
            return View(tamanos);
        }

        // POST: Tamanos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tamanos tamanos = db.Tamanos.Find(id);
            db.Tamanos.Remove(tamanos);
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
