using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ElCuernoVip.Models;
using DTO;
using ElCuernoVip.Utilidades;
using static ElCuernoVip.Models.Enums;


namespace ElCuernoVip.Controllers
{
    public class CategoriasController : Controller
    {
        private VentaCuernoEntities1 db = new VentaCuernoEntities1();

        // GET: Categorias
        public ActionResult Index()
        {
            return View(db.Categorias.ToList());
        }

        // GET: Categorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorias categorias = db.Categorias.Find(id);
            if (categorias == null)
            {
                return HttpNotFound();
            }
            Categoria_DTO categoria_DTO = new Categoria_DTO();
            categoria_DTO.Categoria = categorias.Categoria;
            return View(categoria_DTO);
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria_DTO categorias)
        {
            
            if (ModelState.IsValid)
            {
                Categorias cat = new Categorias();
                cat.Categoria = categorias.Categoria;
                db.Categorias.Add(cat);
                if (db.SaveChanges() == 0)
                {

                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "No se pudo crear la categoria ", NotificationType.warning);

                }
                else
                {
                    //sweet alert
                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Correcto", $"Categoria creada correctamente"
                        , NotificationType.success);
                }
                return RedirectToAction("Index");
            }

            return View(categorias);
        }

        // GET: Categorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorias categorias = db.Categorias.Find(id);
            if (categorias == null)
            {
                return HttpNotFound();
            }
            Categoria_DTO cat_DTO= new Categoria_DTO();
            cat_DTO.Id_Categoria = categorias.Id_Categoria;
            cat_DTO.Categoria=categorias.Categoria;
            return View(cat_DTO);
        }

        // POST: Categorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Categoria,Categoria")] Categorias categorias)
        {
            if (ModelState.IsValid)
            {
                Categoria_DTO categoria_DTO = new Categoria_DTO();
                categoria_DTO.Categoria = categorias.Categoria;
                db.Entry(categorias).State = EntityState.Modified;
                if (db.SaveChanges() == 0)
                {
                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error", "No se pudo actualizar la categoria ", NotificationType.warning);

                }
                else {
                    TempData["sweetAlert"] = SweetAlert.Sweet_Alert("correcto", "Categoria Actualizada ", NotificationType.warning);


                }
                return RedirectToAction("Index");
            }
            return View(categorias);
        }

        // GET: Categorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorias categorias = db.Categorias.Find(id);
            if (categorias == null)
            {

                  return HttpNotFound();
            }
              return View(categorias);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categorias categorias = db.Categorias.Find(id);
            db.Categorias.Remove(categorias);
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
