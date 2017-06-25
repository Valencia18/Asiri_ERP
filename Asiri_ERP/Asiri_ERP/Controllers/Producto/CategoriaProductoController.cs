using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Common.Model;
using Service.Producto;

namespace Asiri_ERP.Controllers.Producto
{
    public class CategoriaProductoController : Controller
    {
        private AsiriContext db = new AsiriContext();

        // GET: CategoriaProducto
        public ActionResult Index()
        {
            var model = new CategoriaBL().List();
            return View(model);
        }

        // GET: CategoriaProducto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROt01_categoria model = new CategoriaBL().GetCategoria(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: CategoriaProducto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriaProducto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCategoria,fecRegistro,fecModificacion,fecEliminacion,idUsuario,idUsuarioModificar,idUsuarioEliminar,nombreCategoria,descCategoria,activo")] PROt01_categoria model)
        {
            if (ModelState.IsValid)
            {
                new CategoriaBL().Add(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: CategoriaProducto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROt01_categoria model = new CategoriaBL().GetCategoria(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: CategoriaProducto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCategoria,fecRegistro,fecModificacion,fecEliminacion,idUsuario,idUsuarioModificar,idUsuarioEliminar,nombreCategoria,descCategoria,activo")] PROt01_categoria model)
        {
            if (ModelState.IsValid)
            {
                new CategoriaBL().Edit(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: CategoriaProducto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROt01_categoria model = db.PROt01_categoria.Find(id);
            new CategoriaBL().ChangeStatus(model);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
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
