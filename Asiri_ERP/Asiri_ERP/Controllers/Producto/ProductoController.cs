using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Common.Helper;
using Common.Model;
using Service.Producto;

namespace Asiri_ERP.Controllers.Producto
{
    public class ProductoController : Controller
    {
        private AsiriContext db = new AsiriContext();

        // GET: Producto
        public ActionResult Index()
        {
            var model = new ProductoBL().List();
            return View(model);
        }

        // GET: Producto/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROt02_producto model = new ProductoBL().GetProduct(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            ViewBag.idCategoria = new SelectList(db.PROt01_categoria, "idCategoria", "nombreCategoria");
            return View();
        }

        // POST: Producto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProducto,codProducto,codBarra,nombreProductoGenerico,nombreProductoComercial,descProducto,obsvProducto,activo,fecRegistro,fecModificacion,fecEliminacion,idUsuario,idUsuarioModificar,idUsuarioEliminar,idCategoria,idMoneda")] PROt02_producto model)
        {
            model.fecRegistro = DateTime.Now;
            if (ModelState.IsValid)
            {
                //Para no perder el id de moneda
                model.idMoneda = Coin.coin.idMoneda;
                new ProductoBL().Add(model);
                return RedirectToAction("Index");
            }

            ViewBag.idCategoria = new SelectList(db.PROt01_categoria, "idCategoria", "nombreCategoria", model.idCategoria);
            return View(model);
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROt02_producto model = new ProductoBL().GetProduct(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCategoria = new SelectList(db.PROt01_categoria, "idCategoria", "nombreCategoria", model.idCategoria);
            return View(model);
        }

        // POST: Producto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProducto,codProducto,codBarra,nombreProductoGenerico,nombreProductoComercial,descProducto,obsvProducto,activo,fecRegistro,fecModificacion,fecEliminacion,idUsuario,idUsuarioModificar,idUsuarioEliminar,idCategoria,idMoneda")] PROt02_producto model)
        {
            if (ModelState.IsValid)
            {
                model.idMoneda = Coin.coin.idMoneda;
                new ProductoBL().Edit(model);
                return RedirectToAction("Index");
            }
            ViewBag.idCategoria = new SelectList(db.PROt01_categoria, "idCategoria", "nombreCategoria", model.idCategoria);
            return View(model);
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROt02_producto model = new ProductoBL().GetProduct(id);
            new ProductoBL().ChangeStatus(model);
            if (model == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }
 
        public ActionResult GetProduct(string id)
        {
            AsiriContext ctx =new AsiriContext();
            ctx.Configuration.ProxyCreationEnabled = false;
            PROt02_producto model = ctx.PROt02_producto.Find(int.Parse(id));
            //PROt02_producto model = new ProductoBL().GetProduct(int.Parse(id));
            return Json(model,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult QuickCreate(string nombre, string desc)
        {
            PROt01_categoria model= new PROt01_categoria(){nombreCategoria = nombre,descCategoria = desc};
            new CategoriaBL().Add(model);
            return Json(model,JsonRequestBehavior.AllowGet);
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
