using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Common.Helper
{
    public class Coin
    {
        public static SNTt03_moneda coin= new Coin().GetCoin();
        public SNTt03_moneda GetCoin()
        {
            using ( AsiriContext context = new AsiriContext())
            {
                try
                {
                    SISt01_parametro paramMoneda = context.SISt01_parametro.SingleOrDefault(pm => pm.codParametro == CodParam.Moneda);
                    SNTt03_moneda moneda = paramMoneda != null ? context.SNTt03_moneda.SingleOrDefault(m => m.idMoneda == paramMoneda.valorNumerico) : null;
                    return moneda;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
