using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ElCuernoVip.Models.Enums;

namespace ElCuernoVip.Utilidades
{
    public class SweetAlert
    {
        public static string Sweet_Alert(string tittle, string msg, NotificationType nt)
        {

            var script = "<script languaje='javascript'>" +
                   "Swal.fire({" +
                   " title: '" + tittle + "'," +
                   " text: '" + msg + "'," +
                   "  icon: '" + nt + "'" +
                   "});" +
                   "</script>";
            return script;
        }
    }
}