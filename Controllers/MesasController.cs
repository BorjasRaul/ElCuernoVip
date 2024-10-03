using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ElCuernoVip.Models;
using ElCuernoVip.Utilidades;
using static ElCuernoVip.Models.Enums;

namespace ElCuernoVip.Controllers
{
    public class MesasController : Controller
    {
        private VentaCuernoEntities1 db = new VentaCuernoEntities1();

        // GET: Mesas
        public ActionResult Index()
        {
            return View(db.Mesas.ToList());
        }

        // GET: Mesas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mesas mesas = db.Mesas.Find(id);
            if (mesas == null)
            {
                return HttpNotFound();
            }
            return View(mesas);
        }

        // GET: Mesas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mesas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Mesa,Codigo_Mesa,Capacidad,Estado")] Mesas mesas)
        {
            if (ModelState.IsValid)
            {
                db.Mesas.Add(mesas);
                if (db.SaveChanges() == 0)
                {
                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "No se pudo crear la mesa ", NotificationType.warning);

                }
                else {

                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("success", "Mesa creada", NotificationType.success);

                }
                return RedirectToAction("Index");
            }

            return View(mesas);
        }

        // GET: Mesas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mesas mesas = db.Mesas.Find(id);
            if (mesas == null)
            {
                return HttpNotFound();
            }
            return View(mesas);
        }

        // POST: Mesas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Mesa,Codigo_Mesa,Capacidad,Estado")] Mesas mesas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mesas).State = EntityState.Modified;
                if (db.SaveChanges() == 0)
                {

                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "No se pudo actualizar la mesa ", NotificationType.warning);

                }
                else {

                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("success", "Mesa Actualizada", NotificationType.success);

                }
                return RedirectToAction("Index");
            }
            return View(mesas);
        }

        // GET: Mesas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mesas mesas = db.Mesas.Find(id);
            if (mesas == null)
            {
                return HttpNotFound();
            }
            return View(mesas);
        }

        // POST: Mesas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mesas mesas = db.Mesas.Find(id);
            db.Mesas.Remove(mesas);
            if (db.SaveChanges() == 0)
            {
                TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "No se pudo eliminar la mesa ", NotificationType.warning);

            }
            else {

                TempData["sweetAlert"] = SweetAlert.Sweet_Alert("success", "Mesa eliminada", NotificationType.success);

            }
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
