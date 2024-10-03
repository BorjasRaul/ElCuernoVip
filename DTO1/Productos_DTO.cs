using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Productos_DTO
    {
        public int ID_Producto { get; set; }
        public string Nombre_Producto { get; set; }
        public string Descripcion_Producto { get; set; }
        public int Categoria_Id { get; set; }
        public int Tamano_Id { get; set; }
        public string Urlfoto_Producto { get; set; }
        public decimal Precio_Producto { get; set; }
    }
}
