using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Login_DTO
    {
        public int Id_Usuario { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Empleado_Id { get; set; }
        public int Cliente_Id { get; set; }
    }
}
