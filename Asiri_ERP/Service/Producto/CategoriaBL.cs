using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Model;
using Persistence.Producto;

namespace Service.Producto
{
    public class CategoriaBL
    {
        public IEnumerable<PROt01_categoria> List()
        {
            return new CategoriaDA().List();
        }
        public PROt01_categoria GetCategoria(long? id)
        {
            return new CategoriaDA().GetCategoria(id);
        }
        public void Add(PROt01_categoria model)
        {
            new CategoriaDA().Add(model);
        }
        public void Edit(PROt01_categoria model)
        {
            new CategoriaDA().Edit(model);
        }
        public void ChangeStatus(PROt01_categoria model)
        {
            new CategoriaDA().ChangeStatus(model);
        }
    }
}
