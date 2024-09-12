using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaOnline.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        public bool HayStock(int cantidad)
        {
            return Stock >= cantidad;
        }

        public void DescontarStock(int cantidad)
        {
            Stock -= cantidad;
        }
    }
}

