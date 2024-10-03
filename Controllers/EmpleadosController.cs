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
    public class EmpleadosController : Controller
    {
        private VentaCuernoEntities1 db = new VentaCuernoEntities1();

        // GET: Empleados
        public ActionResult Index()
        {
            var empleados = db.Empleados.Include(e => e.Roles);
            return View(empleados.ToList());
        }

        // GET: Empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            ViewBag.Rol_Id = new SelectList(db.Roles, "Id_Rol", "Rol");
            return View();
        }

        // POST: Empleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Empleado,Nombre_Empleado,Apeido_P_Empleado,Apeido_M_Empleado,Email_Empleado,Telefono_Empleado,Rol_Id")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Empleados.Add(empleados);
                if (db.SaveChanges() == 0)
                {

                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "No se pudo crear el empleado ", NotificationType.warning);

                }
                else {

                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("success", "Empleado creado ", NotificationType.warning);

                }
                return RedirectToAction("Index");
            }

            ViewBag.Rol_Id = new SelectList(db.Roles, "Id_Rol", "Rol", empleados.Rol_Id);
            return View(empleados);
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            ViewBag.Rol_Id = new SelectList(db.Roles, "Id_Rol", "Rol", empleados.Rol_Id);
            return View(empleados);
        }

        // POST: Empleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Empleado,Nombre_Empleado,Apeido_P_Empleado,Apeido_M_Empleado,Email_Empleado,Telefono_Empleado,Rol_Id")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleados).State = EntityState.Modified;
                if (db.SaveChanges() == 0)
                {
                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "No se pudo Actualizar el empleado ", NotificationType.warning);

                }
                else {

                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("success", "Empleado actualizado", NotificationType.warning);

                }

                return RedirectToAction("Index");
            }
            ViewBag.Rol_Id = new SelectList(db.Roles, "Id_Rol", "Rol", empleados.Rol_Id);
            return View(empleados);
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleados empleados = db.Empleados.Find(id);
            db.Empleados.Remove(empleados);
            if (db.SaveChanges() == 0)
            {
                TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "No se pudo eliminar el empleado ", NotificationType.warning);

            }
            else {
                TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "Empleado eliminado", NotificationType.warning);

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
