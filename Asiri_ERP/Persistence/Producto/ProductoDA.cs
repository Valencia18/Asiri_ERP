using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Validation;
using Common.Model;
using NLog;

namespace Persistence.Producto
{
    public class ProductoDA
    {
        public IEnumerable<PROt02_producto> List()
        {
            using (AsiriContext ctx = new AsiriContext())
            {
                try
                {
                    return ctx.PROt02_producto.Include(p => p.PROt01_categoria).Include(p => p.SNTt03_moneda).ToList();
                }
                catch (Exception ex)
                {
                    Logger log = LogManager.GetLogger("fileLogger");
                    log.Error(ex, "Error al listar productos");
                    return null;
                }
            }
        }
        public PROt02_producto GetProduct(long? id)
        {
            using (AsiriContext ctx = new AsiriContext())
            {
                try
                {
                    return ctx.PROt02_producto.Find(id);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public void Add(PROt02_producto model)
        {
            using (AsiriContext ctx = new AsiriContext())
            {
                try
                {
                    //Añadir la fecha en la que se está registrando el producto
                    model.fecRegistro = DateTime.Today;
                    //registrar id del usuario que registró el producto
                    /***********************************************/
                    model.idUsuario = "34";
                    //Por defecto al registrarse está activo el producto
                    model.activo = true;
                    ctx.PROt02_producto.Add(model);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger log = LogManager.GetLogger("fileLogger");
                    log.Error(ex, "Error al agregar producto");
                }
            }
        }
        public void Edit(PROt02_producto model)
        {
            using (AsiriContext ctx = new AsiriContext())
            {
                try
                {
                    //Añadir usuario que modifica
                    /******************FALTA UNIR CON LO EL USURIO RE ROLES******************/
                    model.idUsuarioModificar = "34";

                    //Añadir fecha en la que se está editando
                    model.fecModificacion = DateTime.Today;
                    ctx.Entry(model).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger log = LogManager.GetLogger("fileLogger");
                    log.Error(ex, "Error al editar el producto");
                }
            }
        }
        public void ChangeStatus(PROt02_producto model)
        {
            try
            {
                using (AsiriContext ctx = new AsiriContext())
                {
                    //Condicional para inverir el estado
                    if (model.activo)
                    {
                        //Evalúa si es true, se ser así se guardará como eliminación lógica
                        model.idUsuarioEliminar = "34";
                        model.fecEliminacion = DateTime.Today;
                        model.activo = false;
                    }
                    else
                    {
                        //Si el estado fue false se activará y se tomará como una modificación
                        model.idUsuarioModificar= "34";
                        model.fecModificacion = DateTime.Today;
                        model.activo = true;
                    }
                    // */*/*/*/   deshabilitado por el momento
                    //ctx.Entry(model).State = EntityState.Modified;
                    //ctx.SaveChanges();
                    int x = model.activo?1:0;
                    // por el momento solo un update ya que genera conflictos, hay que corregir luego
                    ctx.Database.ExecuteSqlCommand($"UPDATE PROt02_producto SET activo = {x} WHERE idProducto = '{model.idProducto}'");
                }
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder st = new StringBuilder();
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        st.AppendLine($"Entidad: \"{eve.Entry.Entity.GetType().Name}\" Estado: \"{eve.Entry.State}\" se tienen los siguientes errores de validación:");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            st.AppendLine($"- Propiedad: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }
                

                Logger log = LogManager.GetLogger("fileLogger");
                log.Error(ex, st.ToString());
            }
        }
    }
}
