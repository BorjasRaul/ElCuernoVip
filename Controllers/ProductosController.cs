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
    public class ProductosController : Controller
    {
        private VentaCuernoEntities1 db = new VentaCuernoEntities1();

        // GET: Productos
        public ActionResult Index()
        {
            var productos = db.Productos.Include(p => p.Categorias).Include(p => p.Tamanos);
            return View(productos.ToList());
        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            ViewBag.Categoria_Id = new SelectList(db.Categorias, "Id_Categoria", "Categoria");
            ViewBag.Tamano_Id = new SelectList(db.Tamanos, "Id_Tamano", "Descripcion");
            return View();
        }

        // POST: Productos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Producto,Nombre_Producto,Descripcion_Producto,Categoria_Id,Tamano_Id,Urlfoto_Producto,Precio_Producto")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                db.Productos.Add(productos);
                if (db.SaveChanges() == 0)
                {
                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "producto no creado ", NotificationType.warning);

                }
                else {

                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("success", "producto creado ", NotificationType.success);

                }
                return RedirectToAction("Index");
            }

            ViewBag.Categoria_Id = new SelectList(db.Categorias, "Id_Categoria", "Categoria", productos.Categoria_Id);
            ViewBag.Tamano_Id = new SelectList(db.Tamanos, "Id_Tamano", "Descripcion", productos.Tamano_Id);
            return View(productos);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categoria_Id = new SelectList(db.Categorias, "Id_Categoria", "Categoria", productos.Categoria_Id);
            ViewBag.Tamano_Id = new SelectList(db.Tamanos, "Id_Tamano", "Descripcion", productos.Tamano_Id);
            return View(productos);
        }

        // POST: Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Producto,Nombre_Producto,Descripcion_Producto,Categoria_Id,Tamano_Id,Urlfoto_Producto,Precio_Producto")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productos).State = EntityState.Modified;
                if (db.SaveChanges() == 0)
                {

                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "producto no actualizado ", NotificationType.warning);
                }
                else {

                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("success", "producto Actualizado", NotificationType.success);

                }

                return RedirectToAction("Index");
            }
            ViewBag.Categoria_Id = new SelectList(db.Categorias, "Id_Categoria", "Categoria", productos.Categoria_Id);
            ViewBag.Tamano_Id = new SelectList(db.Tamanos, "Id_Tamano", "Descripcion", productos.Tamano_Id);
            return View(productos);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Productos productos = db.Productos.Find(id);
            db.Productos.Remove(productos);
            if (db.SaveChanges() == 0)
            {
                TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "producto no Eliminado ", NotificationType.warning);

            }
            else {

                TempData["sweetAlert"] = SweetAlert.Sweet_Alert("success", "producto Eliminado ", NotificationType.success);

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
