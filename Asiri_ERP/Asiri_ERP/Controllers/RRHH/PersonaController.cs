using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Asiri_ERP.Models;
using Common.Model;
using Tecactus.Api.Reniec;


namespace Asiri_ERP.Controllers.RRHH
{
    public class PersonaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Persona
        public ActionResult Index()
        {
            try
            {
                var rHUt09_persona = db.RHUt09_persona.Include(r => r.RHUt05_estadoCivil).Include(r => r.RHUt12_tipoDocIdentidad).Include(r => r.UBIt01_distrito).Include(r => r.UBIt04_via).Include(r => r.UBIt05_zona);
                return View(rHUt09_persona.ToList());
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message + "";
                return View("Error");
                throw;
            }
        }

        public ActionResult Estado(long? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RHUt09_persona rHUt09_persona = db.RHUt09_persona.Find(id);
                if (rHUt09_persona == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    if (rHUt09_persona.activo == false)
                    {
                        rHUt09_persona.activo = true;
                        db.Entry(rHUt09_persona).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        rHUt09_persona.activo = false;
                        //FECHA DE ELIMINACIÓN
                        rHUt09_persona.fecEliminacion = DateTime.Today;
                        db.Entry(rHUt09_persona).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message + "";
                return View("Error");
                throw;
            }
        }


        // GET: Persona/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RHUt09_persona rHUt09_persona = db.RHUt09_persona.Find(id);
            if (rHUt09_persona == null)
            {
                return HttpNotFound();
            }
            return View(rHUt09_persona);
        }



        public ActionResult Create()
        {
            try
            {
                ViewBag.idEstadoCivil = new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil");
                ViewBag.idTipoDocIdentidad = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad");

                

                //SELECT REGION
                ViewBag.idRegion = new SelectList(db., "idRegion", "nombreRegion");
                //SELECT PROVINCIA
                //ViewBag.idProvincia = new SelectList(db.UBIt02_provincia, "idProvincia", "nombreProvincia");
                List<SelectListItem> provs = new List<SelectListItem>();
                //provs.Add(new SelectListItem { Text = "-- Seleccionar --", Value = "0" });
                ViewBag.idProvincia = provs;
                //SELECT DISTRITO
                //ViewBag.idDistrito = new SelectList(db.UBIt01_distrito, "idDistrito", "nombreDistrito");
                List<SelectListItem> list_dists = new List<SelectListItem>();
                //list_dists.Add(new SelectListItem { Text = "-- Seleccionar --", Value = "0" });
                ViewBag.idDistrito = list_dists;

                ViewBag.idVia = new SelectList(db.UBIt04_via, "idVia", "descVia");
                ViewBag.idZona = new SelectList(db.UBIt05_zona, "idZona", "descZona");

                ViewBag.validar = "0";

                return View();

                //ViewBag.idEstadoCivil = new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil");
                //ViewBag.idTipoDocIdentidad = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad");
                //ViewBag.idDistrito = new SelectList(db.UBIt01_distrito, "idDistrito", "nombreDistrito");
                //ViewBag.idVia = new SelectList(db.UBIt04_via, "idVia", "descVia");
                //ViewBag.idZona = new SelectList(db.UBIt05_zona, "idZona", "descZona");
                //return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message + "";
                return View("Error");
                throw;
            }
        }

        [HttpPost]
        public ActionResult Create(RHUt09_persona objPersona, string validar, HttpPostedFileBase pathFoto = null, int idProvincia = 0, int idRegion = 0)
        {
            var valorValidar = validar;
            if (objPersona.sexo != null)
            {
                objPersona.sexo = objPersona.sexo.Substring(0, 1);
            }
            else
            {
                objPersona.sexo = "N";
            }


            if (ModelState.IsValid)
            {
                if (validar == "2")
                {
                    objPersona.sexo = objPersona.sexo;
                    objPersona.difunto = false;
                    objPersona.activo = true;
                    objPersona.fecRegistro = DateTime.Today;
                    objPersona.idUsuario = "1";
                    string idDistritot = objPersona.idDistrito + "";
                    objPersona.idVia = 1;
                    objPersona.idZona = 1;
                    objPersona.idVia = 1;
                    objPersona.idEstadoCivil = 1;
                    db.RHUt09_persona.Add(objPersona);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else if (validar == "1")
                {
                    var idRegionFind1 = idRegion;
                    var idProvinciaFind1 = idProvincia;
                    var idDistrito1 = objPersona.idDistrito;
                    if (idRegionFind1 > 0)
                    {
                        ViewBag.idRegion = new SelectList(db.UBIt03_region, "idRegion", "nombreRegion", idRegion);
                        if (idProvinciaFind1 > 0)
                        {
                            ViewBag.idProvincia = new SelectList(db.UBIt02_provincia, "idProvincia", "nombreProvincia", idProvinciaFind1);
                            if (idDistrito1 > 0)
                            {
                                List<SelectListItem> list_dists = new List<SelectListItem>();
                                ViewBag.idDistrito = new SelectList(db.UBIt01_distrito, "idDistrito", "nombreDistrito", idDistrito1); ;
                            }
                            else
                            {
                                List<SelectListItem> list_dists = new List<SelectListItem>();
                                ViewBag.idDistrito = list_dists;
                            }
                        }
                        else
                        {
                            List<SelectListItem> provs = new List<SelectListItem>();
                            ViewBag.idProvincia = provs;
                            List<SelectListItem> list_dists = new List<SelectListItem>();
                            ViewBag.idDistrito = list_dists;
                        }
                    }
                    else
                    {
                        ViewBag.idRegion = new SelectList(db.UBIt03_region, "idRegion", "nombreRegion");
                        List<SelectListItem> provs = new List<SelectListItem>();
                        ViewBag.idProvincia = provs;
                        List<SelectListItem> list_dists = new List<SelectListItem>();
                        ViewBag.idDistrito = list_dists;
                    }

                    ViewBag.idVia = new SelectList(db.UBIt04_via, "idVia", "descVia");
                    ViewBag.idZona = new SelectList(db.UBIt05_zona, "idZona", "descZona");
                    ViewBag.idEstadoCivil = new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil", objPersona.idEstadoCivil);
                    ViewBag.idTipoDocIdentidad = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad", objPersona.idTipoDocIdentidad);
                    ViewBag.idDistrito = new SelectList(db.UBIt01_distrito, "idDistrito", "nombreDistrito", objPersona.idDistrito);
                    ViewBag.idVia = new SelectList(db.UBIt04_via, "idVia", "descVia", objPersona.idVia);
                    ViewBag.idZona = new SelectList(db.UBIt05_zona, "idZona", "descZona", objPersona.idZona);
                    ViewBag.validar = "2";
                    return View(objPersona);
                }
            }

            ///
            if (validar == "1")
            {
                ViewBag.validar = "0";
            }
            else
            {
                ViewBag.validar = "2";
            }
            var idRegionFind = idRegion;
            var idProvinciaFind = idProvincia;
            var idDistrito = objPersona.idDistrito;
            if (idRegionFind > 0)
            {
                ViewBag.idRegion = new SelectList(db., "idRegion", "nombreRegion", idRegion);
                if (idProvinciaFind > 0)
                {
                    ViewBag.idProvincia = new SelectList(db.UBIt02_provincia, "idProvincia", "nombreProvincia", idProvinciaFind);
                    if (idDistrito > 0)
                    {
                        List<SelectListItem> list_dists = new List<SelectListItem>();
                        ViewBag.idDistrito = new SelectList(db.UBIt01_distrito, "idDistrito", "nombreDistrito", idDistrito); ;
                    }
                    else
                    {
                        List<SelectListItem> list_dists = new List<SelectListItem>();
                        ViewBag.idDistrito = list_dists;
                    }
                }
                else
                {
                    List<SelectListItem> provs = new List<SelectListItem>();
                    ViewBag.idProvincia = provs;
                    List<SelectListItem> list_dists = new List<SelectListItem>();
                    ViewBag.idDistrito = list_dists;
                }
            }
            else
            {
                ViewBag.idRegion = new SelectList(db.UBIt03_region, "idRegion", "nombreRegion");
                List<SelectListItem> provs = new List<SelectListItem>();
                ViewBag.idProvincia = provs;
                List<SelectListItem> list_dists = new List<SelectListItem>();
                ViewBag.idDistrito = list_dists;
            }

            ViewBag.idVia = new SelectList(db.UBIt04_via, "idVia", "descVia");
            ViewBag.idZona = new SelectList(db.UBIt05_zona, "idZona", "descZona");
            ViewBag.idEstadoCivil = new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil", objPersona.idEstadoCivil);
            ViewBag.idTipoDocIdentidad = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad", objPersona.idTipoDocIdentidad);
            //ViewBag.idDistrito = new SelectList(db.UBIt01_distrito, "idDistrito", "nombreDistrito", objPersona.idDistrito);
            ViewBag.idVia = new SelectList(db.UBIt04_via, "idVia", "descVia", objPersona.idVia);
            ViewBag.idZona = new SelectList(db.UBIt05_zona, "idZona", "descZona", objPersona.idZona);
            return View(objPersona);

        }






        // GET: Persona/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RHUt09_persona rHUt09_persona = db.RHUt09_persona.Find(id);
            if (rHUt09_persona == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEstadoCivil = new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil", rHUt09_persona.idEstadoCivil);
            ViewBag.idTipoDocIdentidad = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "codTipoDocIdentidad", rHUt09_persona.idTipoDocIdentidad);
            ViewBag.idDistrito = new SelectList(db.UBIt01_distrito, "idDistrito", "nombreDistrito", rHUt09_persona.idDistrito);
            ViewBag.idVia = new SelectList(db.UBIt04_via, "idVia", "descVia", rHUt09_persona.idVia);
            ViewBag.idZona = new SelectList(db.UBIt05_zona, "idZona", "descZona", rHUt09_persona.idZona);
            return View(rHUt09_persona);
        }

        // POST: Persona/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPersona,tipoPersoneria,nombreRepresentante,nombrePersona,apellidoPaterno,apellidoMaterno,numDocIdentidad,razonSocial,fecNacimiento,nombreVia,numVia,nombreZona,direccion01,direccion02,numTelefonico01,numTelefonico02,email01,email02,sexo,difunto,fecDefuncion,pathFoto,activo,fecRegistro,fecModificacion,fecEliminacion,idUsuario,idUsuarioModificar,idUsuarioEliminar,idVia,idZona,idTipoDocIdentidad,idDistrito,idEstadoCivil,obsvPersona")] RHUt09_persona rHUt09_persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rHUt09_persona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEstadoCivil = new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil", rHUt09_persona.idEstadoCivil);
            ViewBag.idTipoDocIdentidad = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "codTipoDocIdentidad", rHUt09_persona.idTipoDocIdentidad);
            ViewBag.idDistrito = new SelectList(db.UBIt01_distrito, "idDistrito", "nombreDistrito", rHUt09_persona.idDistrito);
            ViewBag.idVia = new SelectList(db.UBIt04_via, "idVia", "descVia", rHUt09_persona.idVia);
            ViewBag.idZona = new SelectList(db.UBIt05_zona, "idZona", "descZona", rHUt09_persona.idZona);
            return View(rHUt09_persona);
        }

        // GET: Persona/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RHUt09_persona rHUt09_persona = db.RHUt09_persona.Find(id);
            if (rHUt09_persona == null)
            {
                return HttpNotFound();
            }
            return View(rHUt09_persona);
        }

        // POST: Persona/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            RHUt09_persona rHUt09_persona = db.RHUt09_persona.Find(id);
            db.RHUt09_persona.Remove(rHUt09_persona);
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
