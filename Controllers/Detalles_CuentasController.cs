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
    public class Detalles_CuentasController : Controller
    {
        private VentaCuernoEntities1 db = new VentaCuernoEntities1();

        // GET: Detalles_Cuentas
        public ActionResult Index()
        {
            var detalles_Cuentas = db.Detalles_Cuentas.Include(d => d.Cuentas).Include(d => d.Productos);
            return View(detalles_Cuentas.ToList());
        }

        // GET: Detalles_Cuentas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalles_Cuentas detalles_Cuentas = db.Detalles_Cuentas.Find(id);
            if (detalles_Cuentas == null)
            {
                return HttpNotFound();
            }
            return View(detalles_Cuentas);
        }

        // GET: Detalles_Cuentas/Create
        public ActionResult Create()
        {
            ViewBag.Venta_Id = new SelectList(db.Cuentas, "Id_Cuenta", "Id_Cuenta");
            ViewBag.Producto_Id = new SelectList(db.Productos, "ID_Producto", "Nombre_Producto");
            return View();
        }

        // POST: Detalles_Cuentas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Detalle_Cuenta,Venta_Id,Producto_Id,Cantidad,Subtotal")] Detalles_Cuentas detalles_Cuentas)
        {
            if (ModelState.IsValid)
            {
                db.Detalles_Cuentas.Add(detalles_Cuentas);
                if (db.SaveChanges() == 0)
                {

                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "No se pudo crear el detalle de la cuenta ", NotificationType.warning);

                }
                else {

                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Success", "Detalle de cuenta creado ", NotificationType.warning);

                }
                return RedirectToAction("Index");
            }

            ViewBag.Venta_Id = new SelectList(db.Cuentas, "Id_Cuenta", "Id_Cuenta", detalles_Cuentas.Venta_Id);
            ViewBag.Producto_Id = new SelectList(db.Productos, "ID_Producto", "Nombre_Producto", detalles_Cuentas.Producto_Id);
            return View(detalles_Cuentas);
        }

        // GET: Detalles_Cuentas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalles_Cuentas detalles_Cuentas = db.Detalles_Cuentas.Find(id);
            if (detalles_Cuentas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Venta_Id = new SelectList(db.Cuentas, "Id_Cuenta", "Id_Cuenta", detalles_Cuentas.Venta_Id);
            ViewBag.Producto_Id = new SelectList(db.Productos, "ID_Producto", "Nombre_Producto", detalles_Cuentas.Producto_Id);
            return View(detalles_Cuentas);
        }

        // POST: Detalles_Cuentas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Detalle_Cuenta,Venta_Id,Producto_Id,Cantidad,Subtotal")] Detalles_Cuentas detalles_Cuentas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalles_Cuentas).State = EntityState.Modified;
                if (db.SaveChanges() == 0)
                {

                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "No se pudo Actualizar el detalle de cuenta ", NotificationType.warning);

                }
                else {

                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("succes", "Detalle de la cuenta actualizado ", NotificationType.warning);

                }
                return RedirectToAction("Index");
            }
            ViewBag.Venta_Id = new SelectList(db.Cuentas, "Id_Cuenta", "Id_Cuenta", detalles_Cuentas.Venta_Id);
            ViewBag.Producto_Id = new SelectList(db.Productos, "ID_Producto", "Nombre_Producto", detalles_Cuentas.Producto_Id);
            return View(detalles_Cuentas);
        }

        // GET: Detalles_Cuentas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalles_Cuentas detalles_Cuentas = db.Detalles_Cuentas.Find(id);
            if (detalles_Cuentas == null)
            {
                return HttpNotFound();
            }
            return View(detalles_Cuentas);
        }

        // POST: Detalles_Cuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Detalles_Cuentas detalles_Cuentas = db.Detalles_Cuentas.Find(id);
            db.Detalles_Cuentas.Remove(detalles_Cuentas);
            if (db.SaveChanges() == 0)
            {

                TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "No se pudo eliminar el detalle de la cuenta ", NotificationType.warning);

            }
            else {
                TempData["sweetAlert"] = SweetAlert.Sweet_Alert("success", "Detalle de la cuenta eliminado", NotificationType.warning);

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
