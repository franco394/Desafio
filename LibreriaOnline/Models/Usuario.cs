using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibreriaOnline.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }

        public bool Autenticar(string email, string contrasena)
        {
            return Email == email && Contrasena == contrasena;
        }
    }
}
