using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Detalle_Cuentas_DTO
    {
        public int Id_Detalle_Cuenta { get; set; }
        public int Venta_Id { get; set; }
        public int Producto_Id { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }

    }
}
