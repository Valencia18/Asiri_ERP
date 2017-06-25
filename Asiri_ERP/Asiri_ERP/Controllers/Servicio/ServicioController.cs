using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Common.Model;

namespace Asiri_ERP.Controllers.ProductoSerivicio
{
    public class ServicioController : Controller
    {
        private AsiriContext db = new AsiriContext();

        // GET: Servicio
        public ActionResult Index()
        {
            var pROt04_servicio = db.PROt04_servicio.Include(p => p.PROt05_tipoDeServicio).Include(p => p.RHUt04_especialidad);
            return View(pROt04_servicio.ToList());
        }

        // GET: Servicio/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROt04_servicio pROt04_servicio = db.PROt04_servicio.Find(id);
            if (pROt04_servicio == null)
            {
                return HttpNotFound();
            }
            return View(pROt04_servicio);
        }

        // GET: Servicio/Create
        public ActionResult Create()
        {
            ViewBag.idTipoDeServicio = new SelectList(db.PROt05_tipoDeServicio, "idTipoDeServicio", "descTipoDeServicio");
            ViewBag.idEspecialidad = new SelectList(db.RHUt04_especialidad, "idEspecialidad", "nombreEspecialidad");
            return View();
        }

        // POST: Servicio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idServicio,codServicio,nombreServicio,descServicio,precio,precioMinimo,precioMaximo,esGratis,activo,fecRegistro,fecModificacion,fecEliminacion,idUsuario,idUsuarioModificar,idUsuarioEliminar,idTipoDeServicio,idEspecialidad")] PROt04_servicio pROt04_servicio)
        {
            if (ModelState.IsValid)
            {
                db.PROt04_servicio.Add(pROt04_servicio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idTipoDeServicio = new SelectList(db.PROt05_tipoDeServicio, "idTipoDeServicio", "descTipoDeServicio", pROt04_servicio.idTipoDeServicio);
            ViewBag.idEspecialidad = new SelectList(db.RHUt04_especialidad, "idEspecialidad", "nombreEspecialidad", pROt04_servicio.idEspecialidad);
            return View(pROt04_servicio);
        }

        // GET: Servicio/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROt04_servicio pROt04_servicio = db.PROt04_servicio.Find(id);
            if (pROt04_servicio == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTipoDeServicio = new SelectList(db.PROt05_tipoDeServicio, "idTipoDeServicio", "descTipoDeServicio", pROt04_servicio.idTipoDeServicio);
            ViewBag.idEspecialidad = new SelectList(db.RHUt04_especialidad, "idEspecialidad", "nombreEspecialidad", pROt04_servicio.idEspecialidad);
            return View(pROt04_servicio);
        }

        // POST: Servicio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idServicio,codServicio,nombreServicio,descServicio,precio,precioMinimo,precioMaximo,esGratis,activo,fecRegistro,fecModificacion,fecEliminacion,idUsuario,idUsuarioModificar,idUsuarioEliminar,idTipoDeServicio,idEspecialidad")] PROt04_servicio pROt04_servicio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pROt04_servicio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTipoDeServicio = new SelectList(db.PROt05_tipoDeServicio, "idTipoDeServicio", "descTipoDeServicio", pROt04_servicio.idTipoDeServicio);
            ViewBag.idEspecialidad = new SelectList(db.RHUt04_especialidad, "idEspecialidad", "nombreEspecialidad", pROt04_servicio.idEspecialidad);
            return View(pROt04_servicio);
        }

        // GET: Servicio/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROt04_servicio pROt04_servicio = db.PROt04_servicio.Find(id);
            if (pROt04_servicio == null)
            {
                return HttpNotFound();
            }
            return View(pROt04_servicio);
        }

        // POST: Servicio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PROt04_servicio pROt04_servicio = db.PROt04_servicio.Find(id);
            db.PROt04_servicio.Remove(pROt04_servicio);
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
