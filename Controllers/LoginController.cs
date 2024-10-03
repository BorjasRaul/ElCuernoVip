using ElCuernoVip.Models;
using ElCuernoVip.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static ElCuernoVip.Models.Enums;

namespace ElCuernoVip.Controllers
{
    public class LoginController : Controller
    {
        private VentaCuernoEntities1 db = new VentaCuernoEntities1();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        //Post Login
        [HttpPost]
        [ValidateAntiForgeryToken] //se encarga de recibir el token generado por la peticion de la vista y en caso de que no coincida niega la peticion
        public ActionResult Login(Usuarios user)
        {

            try
            {
                using (VentaCuernoEntities1 context = new VentaCuernoEntities1())
                {

                    var hashedPass = ComputeSha512Hash(user.Password);
                    //voy a buscar dentro del usuario que tenga tanto el nombre como la contraseña
                    Usuarios original = (from u in context.Usuarios
                                         where u.Username.Trim().ToUpper() ==
                                         user.Username.Trim().ToLower()
                                         && u.Password.Trim().ToUpper() == user.Password.Trim().ToUpper()
                                         select u).FirstOrDefault();

                    //valido que realmente hayya encontrado a mi usuario

                    if (original != null)
                    {

                        //declaro mis variables de sesion
                        Session["Nickname"] = original.Username;
                        Session["rol"] = original.Empleado_Id;
                        //sweet alert
                        TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Bienvenido", $"Bienvenid@ de vuelta {original.Username}"
                            , NotificationType.success);
                        return RedirectToAction("Index", "Camiones");
                    }
                    else
                    {

                        //por seguridad limpio mis variables de sesion
                        Session.RemoveAll();
                        Session.Clear();
                        TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Alto", " usuario y/o contraseña incorrecta ", NotificationType.warning);
                        return RedirectToAction("Index");


                    }

                }
            }
            catch (HttpAntiForgeryException ex)
            {

                Session.RemoveAll();
                Session.Clear();
                TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Error ", ex.Message, NotificationType.error);
                return RedirectToAction("Login");

            }

        }

        //get Logout

        public ActionResult Logout()
        {

            Session.RemoveAll();
            Session.Clear();
            TempData["sweetAlert"] = SweetAlert.Sweet_Alert("Hasta luego", " ", NotificationType.info);
            return RedirectToAction("Index");

        }


        private static string ComputeSha512Hash(string rawData)
        {
            //Raw Data (Pronunciado: roh –dei-ta) es todo tipo de data que no ha sido procesada aún.
            using (SHA512 sha512Hash = SHA512.Create())
            {
                //ComputeHash => deolver el array de bytes de la palabra ya cifrada
                byte[] bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                //convertimos el array a un nuevo string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    //La cadena de formato "x2" en el método ToString se utiliza para convertir un byte a su representación hexadecimal, asegurando que cada byte se represente con dos caracteres:

                    //"x": Especifica que el formato de la cadena debe ser hexadecimal en minúsculas.
                    //"2": Indica que la cadena resultante debe tener al menos dos caracteres.Si el valor hexadecimal del byte es un solo carácter(por ejemplo, 0x5), se agregará un cero a la izquierda para hacer dos caracteres(05).
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}