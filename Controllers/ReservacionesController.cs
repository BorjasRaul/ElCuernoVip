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
    public class ReservacionesController : Controller
    {
        private VentaCuernoEntities1 db = new VentaCuernoEntities1();

        // GET: Reservaciones
        public ActionResult Index()
        {
            var reservaciones = db.Reservaciones.Include(r => r.Clientes).Include(r => r.Mesas);
            return View(reservaciones.ToList());
        }

        // GET: Reservaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservaciones reservaciones = db.Reservaciones.Find(id);
            if (reservaciones == null)
            {
                return HttpNotFound();
            }
            return View(reservaciones);
        }

        // GET: Reservaciones/Create
        public ActionResult Create()
        {
            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id_Cliente", "Nombre_Cliente");
            ViewBag.Mesa_Id = new SelectList(db.Mesas, "ID_Mesa", "Codigo_Mesa");
            return View();
        }

        // POST: Reservaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Reservacion,Mesa_Id,Cliente_Id,Fecha_Reservacion,Horario,Num_Personas,Estatus")] Reservaciones reservaciones)
        {
            if (ModelState.IsValid)
            {
                db.Reservaciones.Add(reservaciones);
                if (db.SaveChanges() == 0)
                {
                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "Reservacion no creada ", NotificationType.warning);

                }
                else {
                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("success", "Reservacion creada ", NotificationType.success);

                }
                return RedirectToAction("Index");
            }

            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id_Cliente", "Nombre_Cliente", reservaciones.Cliente_Id);
            ViewBag.Mesa_Id = new SelectList(db.Mesas, "ID_Mesa", "Codigo_Mesa", reservaciones.Mesa_Id);
            return View(reservaciones);
        }

        // GET: Reservaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservaciones reservaciones = db.Reservaciones.Find(id);
            if (reservaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id_Cliente", "Nombre_Cliente", reservaciones.Cliente_Id);
            ViewBag.Mesa_Id = new SelectList(db.Mesas, "ID_Mesa", "Codigo_Mesa", reservaciones.Mesa_Id);
            return View(reservaciones);
        }

        // POST: Reservaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Reservacion,Mesa_Id,Cliente_Id,Fecha_Reservacion,Horario,Num_Personas,Estatus")] Reservaciones reservaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservaciones).State = EntityState.Modified;
                if (db.SaveChanges() == 0)
                {
                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "Reservacion no Actualizado ", NotificationType.warning);

                }
                else {

                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("success", "Reservacion Actualizada", NotificationType.success);

                }
                return RedirectToAction("Index");
            }
            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id_Cliente", "Nombre_Cliente", reservaciones.Cliente_Id);
            ViewBag.Mesa_Id = new SelectList(db.Mesas, "ID_Mesa", "Codigo_Mesa", reservaciones.Mesa_Id);
            return View(reservaciones);
        }

        // GET: Reservaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservaciones reservaciones = db.Reservaciones.Find(id);
            if (reservaciones == null)
            {
                return HttpNotFound();
            }
            return View(reservaciones);
        }

        // POST: Reservaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservaciones reservaciones = db.Reservaciones.Find(id);
            db.Reservaciones.Remove(reservaciones);
            if (db.SaveChanges() == 0)
            {
                TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "producto no Eliminado ", NotificationType.warning);

            }
            else {

                TempData["sweetAlert"] = SweetAlert.Sweet_Alert("success", "Producto Eliminado ", NotificationType.success);

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
