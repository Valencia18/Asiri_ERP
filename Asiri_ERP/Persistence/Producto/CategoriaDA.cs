using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using NLog;
using System.Data.Entity;

namespace Persistence.Producto
{
    public class CategoriaDA
    {
        public List<PROt01_categoria> List()
        {
            using (var ctx  = new AsiriContext())
            {
                try
                {
                    return ctx.PROt01_categoria.ToList();
                }
                catch (Exception ex)
                {
                    Logger log = LogManager.GetLogger("fileLogger");
                    log.Error(ex,"Error al listar Categorías de productos");
                    return null;
                }
            }
        }
        public PROt01_categoria GetCategoria(long? id)
        {
            using (var ctx = new AsiriContext())
            {
                try
                {
                    return ctx.PROt01_categoria.Find(id);
                }
                catch (Exception ex)
                {
                    Logger log = LogManager.GetLogger("fileLogger");
                    log.Error(ex,"Error al al obtener una categoría");
                    return null;
                }
            }
        }
        public void Add(PROt01_categoria model)
        {
            using(var ctx = new AsiriContext())
            {
                try
                {
                    //por defecto cuando se crea la categoría está activo
                    model.activo = true;
                    //Se registra el usuario que crea la categoría
                    /***************  Corregir con roles  **************/
                    model.idUsuario = "32";
                    ctx.PROt01_categoria.Add(model);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger log = LogManager.GetLogger("fileLogger");
                    log.Error(ex, "Error al crear una categoría de productos");
                }
            }
        }
        public void Edit(PROt01_categoria model)
        {
            using (var ctx = new AsiriContext())
            {
                try
                {
                    //Se indica qué usuario modificó la categoría
                    /*************   corregir con roles      ***************/
                    model.idUsuarioModificar = "32";
                    ctx.PROt01_categoria.Add(model);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger log = LogManager.GetLogger("fileLogger");
                    log.Error(ex , "Error al Editar una categoría de productoas");
                }
            }
        }
        public void ChangeStatus(PROt01_categoria model)
        {
            using (var ctx = new AsiriContext())
            {
                try
                {
                    //Condicional para inverir el estado
                    if (model.activo)
                    {
                        //Evalúa si es true, se ser así se guardará como eliminación lógica
                        model.idUsuarioEliminar= "32";
                        model.fecEliminacion = DateTime.Today;
                        model.activo = false;
                    }
                    else
                    {
                        //Si el estado fue false se activará y se tomará como una modificación
                        model.idUsuarioModificar = "32";
                        model.fecModificacion = DateTime.Today;
                        model.activo = true;
                    }
                    ctx.Entry(model).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger log = LogManager.GetLogger("fileLogger");
                    log.Error(ex, "Error al cambiar el estado de una categoría de productos");
                } 
            }
        }
    }
}
