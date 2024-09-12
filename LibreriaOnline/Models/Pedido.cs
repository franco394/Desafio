using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;

using System;
using System.Collections.Generic;
using System.Linq;

namespace LibreriaOnline.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public List<DetallePedido> Detalles { get; set; }
        public decimal Total { get; set; }

        public Pedido()
        {
            Detalles = new List<DetallePedido>();
        }

        public void AgregarDetalle(Libro libro, int cantidad)
        {
            Detalles.Add(new DetallePedido
            {
                Libro = libro,
                Cantidad = cantidad,
                PrecioUnitario = libro.Precio
            });
        }

        public void CalcularTotal()
        {
            Total = Detalles.Sum(d => d.Cantidad * d.PrecioUnitario);
        }
    }

    public class DetallePedido
    {
        public int Id { get; set; }
        public Libro Libro { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
