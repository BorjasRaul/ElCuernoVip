using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Cuentas_DTO
    {
        public int Id_Cuenta { get; set; }
        public Nullable<int> Mesa_Id { get; set; }
        public Nullable<int> Cliente_Id { get; set; }
        public Nullable<int> Reservacion_Id { get; set; }
        public Nullable<int> Empleado_Id { get; set; }
        public System.DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public bool Es_Reservacion { get; set; }

   

    }
}
