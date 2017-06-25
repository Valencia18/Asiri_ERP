using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Common.Model;
using AsiriContext = Common.AsiriContext;

namespace AsiriERP.Controllers.Servicio
{
    public class TipoServicioController : Controller
    {
        private AsiriContext db = new AsiriContext();

        // GET: TipoServicio
        public ActionResult Index()
        {
            return View(db.PROt05_tipoDeServicio.ToList());
        }

        // GET: TipoServicio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROt05_tipoDeServicio pROt05_tipoDeServicio = db.PROt05_tipoDeServicio.Find(id);
            if (pROt05_tipoDeServicio == null)
            {
                return HttpNotFound();
            }
            return View(pROt05_tipoDeServicio);
        }

        // GET: TipoServicio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoServicio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTipoDeServicio,descTipoDeServicio,activo,fecRegistro,fecModificacion,fecEliminacion,idUsuario,idUsuarioModificar,idUsuarioEliminar")] PROt05_tipoDeServicio pROt05_tipoDeServicio)
        {
            if (ModelState.IsValid)
            {
                db.PROt05_tipoDeServicio.Add(pROt05_tipoDeServicio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pROt05_tipoDeServicio);
        }

        // GET: TipoServicio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROt05_tipoDeServicio pROt05_tipoDeServicio = db.PROt05_tipoDeServicio.Find(id);
            if (pROt05_tipoDeServicio == null)
            {
                return HttpNotFound();
            }
            return View(pROt05_tipoDeServicio);
        }

        // POST: TipoServicio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTipoDeServicio,descTipoDeServicio,activo,fecRegistro,fecModificacion,fecEliminacion,idUsuario,idUsuarioModificar,idUsuarioEliminar")] PROt05_tipoDeServicio pROt05_tipoDeServicio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pROt05_tipoDeServicio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pROt05_tipoDeServicio);
        }

        // GET: TipoServicio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROt05_tipoDeServicio pROt05_tipoDeServicio = db.PROt05_tipoDeServicio.Find(id);
            if (pROt05_tipoDeServicio == null)
            {
                return HttpNotFound();
            }
            return View(pROt05_tipoDeServicio);
        }

        // POST: TipoServicio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PROt05_tipoDeServicio pROt05_tipoDeServicio = db.PROt05_tipoDeServicio.Find(id);
            db.PROt05_tipoDeServicio.Remove(pROt05_tipoDeServicio);
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
