using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DTO;
using ElCuernoVip.Models;
using ElCuernoVip.Utilidades;
using static ElCuernoVip.Models.Enums;

namespace ElCuernoVip.Controllers
{
    public class ClientesController : Controller
    {
        private VentaCuernoEntities1 db = new VentaCuernoEntities1();

        // GET: Clientes
        public ActionResult Index()
        {
            
            return View(db.Clientes.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
           
            return View(clientes);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Cliente,Nombre_Cliente,Apeido_P_Cliente,Apeido_M_Cliente,Email,Telefono")] Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                
                db.Clientes.Add(clientes);
                if (db.SaveChanges() == 0)
                {
                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "Cliente no creado ", NotificationType.warning);
                }
                else {
                    //sweet alert
                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Correcto", $"Cliente creado correctamente"
                        , NotificationType.success);
                }


                return RedirectToAction("Index");
            }

            return View(clientes);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
               
                return HttpNotFound();
            }
             return View(clientes);
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Cliente,Nombre_Cliente,Apeido_P_Cliente,Apeido_M_Cliente,Email,Telefono")] Clientes clientes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(clientes).State = EntityState.Modified;
                    if (db.SaveChanges() == 0)
                    {
                        TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "No se pudo actualizar ", NotificationType.warning);

                    }
                    else
                    {
                        TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Correcto", $"Cliente Actualizado correctamente"
                         , NotificationType.success);
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "Cliente no actualizado ", NotificationType.error);

            }
            return View(clientes);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Clientes clientes = db.Clientes.Find(id);
                db.Clientes.Remove(clientes);
                if (db.SaveChanges() == 0)
                {

                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "Cliente no Eliminado ", NotificationType.warning);

                }
                else
                {
                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Correcto", $"Cliente eliminado correctamente"
                             , NotificationType.success);

                }
            }
            catch (Exception ex)
            {
                TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "Cliente no Eliminado ", NotificationType.error);

               
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
