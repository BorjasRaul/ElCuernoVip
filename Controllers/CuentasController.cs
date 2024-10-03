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
    public class CuentasController : Controller
    {
        private VentaCuernoEntities1 db = new VentaCuernoEntities1();

        // GET: Cuentas
        public ActionResult Index()
        {
            var cuentas = db.Cuentas.Include(c => c.Clientes).Include(c => c.Empleados).Include(c => c.Mesas).Include(c => c.Reservaciones);
            return View(cuentas.ToList());
        }

        // GET: Cuentas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuentas cuentas = db.Cuentas.Find(id);
            if (cuentas == null)
            {
                return HttpNotFound();
            }
            return View(cuentas);
        }

        // GET: Cuentas/Create
        public ActionResult Create()
        {
            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id_Cliente", "Nombre_Cliente");
            ViewBag.Empleado_Id = new SelectList(db.Empleados, "Id_Empleado", "Nombre_Empleado");
            ViewBag.Mesa_Id = new SelectList(db.Mesas, "ID_Mesa", "Codigo_Mesa");
            ViewBag.Reservacion_Id = new SelectList(db.Reservaciones, "Id_Reservacion", "Estatus");
            return View();
        }

        // POST: Cuentas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Cuenta,Mesa_Id,Cliente_Id,Reservacion_Id,Empleado_Id,Fecha,Total,Es_Reservacion")] Cuentas cuentas)
        {
            if (ModelState.IsValid)
            {
                db.Cuentas.Add(cuentas);
                if (db.SaveChanges() == 0)
                {
                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "No se pudo crear la cuenta ", NotificationType.warning);

                }
                else {
                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Success", "Cuenta creada correctamente ", NotificationType.warning);

                }
                return RedirectToAction("Index");
            }

            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id_Cliente", "Nombre_Cliente", cuentas.Cliente_Id);
            ViewBag.Empleado_Id = new SelectList(db.Empleados, "Id_Empleado", "Nombre_Empleado", cuentas.Empleado_Id);
            ViewBag.Mesa_Id = new SelectList(db.Mesas, "ID_Mesa", "Codigo_Mesa", cuentas.Mesa_Id);
            ViewBag.Reservacion_Id = new SelectList(db.Reservaciones, "Id_Reservacion", "Estatus", cuentas.Reservacion_Id);
            return View(cuentas);
        }

        // GET: Cuentas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuentas cuentas = db.Cuentas.Find(id);
            if (cuentas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id_Cliente", "Nombre_Cliente", cuentas.Cliente_Id);
            ViewBag.Empleado_Id = new SelectList(db.Empleados, "Id_Empleado", "Nombre_Empleado", cuentas.Empleado_Id);
            ViewBag.Mesa_Id = new SelectList(db.Mesas, "ID_Mesa", "Codigo_Mesa", cuentas.Mesa_Id);
            ViewBag.Reservacion_Id = new SelectList(db.Reservaciones, "Id_Reservacion", "Estatus", cuentas.Reservacion_Id);
            return View(cuentas);
        }

        // POST: Cuentas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Cuenta,Mesa_Id,Cliente_Id,Reservacion_Id,Empleado_Id,Fecha,Total,Es_Reservacion")] Cuentas cuentas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuentas).State = EntityState.Modified;
                if (db.SaveChanges() == 0)
                {
                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "No se pudo actualizar la cuenta ", NotificationType.warning);

                }
                else {
                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("success", "Cuenta Actualizada ", NotificationType.warning);

                }
                return RedirectToAction("Index");
            }
            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id_Cliente", "Nombre_Cliente", cuentas.Cliente_Id);
            ViewBag.Empleado_Id = new SelectList(db.Empleados, "Id_Empleado", "Nombre_Empleado", cuentas.Empleado_Id);
            ViewBag.Mesa_Id = new SelectList(db.Mesas, "ID_Mesa", "Codigo_Mesa", cuentas.Mesa_Id);
            ViewBag.Reservacion_Id = new SelectList(db.Reservaciones, "Id_Reservacion", "Estatus", cuentas.Reservacion_Id);
            return View(cuentas);
        }

        // GET: Cuentas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuentas cuentas = db.Cuentas.Find(id);
            if (cuentas == null)
            {
                return HttpNotFound();
            }
            return View(cuentas);
        }

        // POST: Cuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cuentas cuentas = db.Cuentas.Find(id);
            db.Cuentas.Remove(cuentas);
            if (db.SaveChanges() == 0)
            {
                TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "No se pudo eliminar la categoria ", NotificationType.warning);

            }
            else {
                TempData["sweetAlert"] = SweetAlert.Sweet_Alert("success", "Cuenta eliminada ", NotificationType.warning);

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
