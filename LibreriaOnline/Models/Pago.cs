using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

using System;

using System;
using System.Globalization;

namespace LibreriaOnline.Models
{
    public class Pago
    {
        public int Id { get; set; }
        public Pedido Pedido { get; set; }
        public string TarjetaNumero { get; set; }
        public DateTime TarjetaVencimiento { get; set; }
        public string TarjetaCodigoSeguridad { get; set; }

        public bool VerificarTarjeta()
        {
            bool esNumeroValido = ValidarNumeroTarjeta(TarjetaNumero);
            bool esVencimientoValido = ValidarFechaVencimiento(TarjetaVencimiento);
            bool esCodigoValido = ValidarCodigoSeguridad(TarjetaCodigoSeguridad);

            if (!esNumeroValido)
                Console.WriteLine("Número de tarjeta inválido. Debe tener 16 dígitos y solo contener números.");

            if (!esVencimientoValido)
                Console.WriteLine("Fecha de vencimiento inválida o la tarjeta ya está vencida.");

            if (!esCodigoValido)
                Console.WriteLine("Código de seguridad inválido. Debe tener 3 dígitos.");

            return esNumeroValido && esVencimientoValido && esCodigoValido;
        }

        
        private bool ValidarNumeroTarjeta(string numero)
        {
            return !string.IsNullOrEmpty(numero) && numero.Length == 16 && long.TryParse(numero, out _);
        }

       
        private bool ValidarFechaVencimiento(DateTime fechaVencimiento)
        {
            return fechaVencimiento > DateTime.Now;
        }

      
        private bool ValidarCodigoSeguridad(string codigo)
        {
            return !string.IsNullOrEmpty(codigo) && codigo.Length == 3 && int.TryParse(codigo, out _);
        }

   
        public void ProcesarPago()
        {
            if (VerificarTarjeta())
            {
                Console.WriteLine("Pago procesado correctamente.");
            }
            else
            {
                Console.WriteLine("Error en la verificación de la tarjeta. El pago no se puede procesar.");
            }
        }
    }
}


