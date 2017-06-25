using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Persistence.Producto;

namespace Service.Producto
{
    public class ProductoBL
    {
        public IEnumerable<PROt02_producto> List()
        {
            return new ProductoDA().List();
        }
        public PROt02_producto GetProduct(long? id)
        {
            return new ProductoDA().GetProduct(id);
        }
        public void Add(PROt02_producto model)
        {
            new ProductoDA().Add(model);
        }
        public void Edit (PROt02_producto model)
        {
            new ProductoDA().Edit(model);
        }
        public void ChangeStatus(PROt02_producto model)
        {
            new ProductoDA().ChangeStatus(model);
        }
    }
}
