using System;
using System.Collections.Generic;
using LibreriaOnline.Models;

class Program
{
    static List<Usuario> usuariosRegistrados = new List<Usuario>();

    static void Main(string[] args)
    {
        
        InicializarUsuarios();

        Console.WriteLine("Bienvenido a la Librería Online.");

        Usuario usuario = null;

        while (usuario == null)
        {
            Console.WriteLine("1. Iniciar sesión");
            Console.WriteLine("2. Registrarse");
            Console.WriteLine("Seleccione una opción (1/2):");
            string opcion = Console.ReadLine();

            if (opcion == "1")
            {
                usuario = Login();
            }
            else if (opcion == "2")
            {
                usuario = Registrar();
            }
            else
            {
                Console.WriteLine("Opción inválida. Inténtelo de nuevo.");
            }
        }

        
        List<Libro> catalogo = InicializarCatalogo();
        MostrarCatalogo(catalogo);

    
        Pedido pedido = new Pedido { Usuario = usuario, Fecha = DateTime.Now };

        
        while (true)
        {
            Console.WriteLine("\nIngrese el ID del libro que desea comprar (o 0 para finalizar):");
            int libroId = int.Parse(Console.ReadLine());

            if (libroId == 0) break;

            Libro libroSeleccionado = catalogo.Find(l => l.Id == libroId);

            if (libroSeleccionado == null)
            {
                Console.WriteLine("Libro no encontrado.");
                continue;
            }

            Console.WriteLine("Ingrese la cantidad que desea comprar:");
            int cantidad = int.Parse(Console.ReadLine());

            if (libroSeleccionado.HayStock(cantidad))
            {
                pedido.AgregarDetalle(libroSeleccionado, cantidad);
                Console.WriteLine($"Agregaste {cantidad} de '{libroSeleccionado.Titulo}' al carrito.");
            }
            else
            {
                Console.WriteLine("Stock insuficiente.");
            }
        }

       
        pedido.CalcularTotal();
        Console.WriteLine("\nResumen del pedido:");
        foreach (var detalle in pedido.Detalles)
        {
            Console.WriteLine($"{detalle.Cantidad} x {detalle.Libro.Titulo} - ${detalle.PrecioUnitario}");
        }
        Console.WriteLine($"Total: ${pedido.Total}");

        Console.WriteLine("\n¿Desea confirmar la compra? (s/n)");
        string confirmacion = Console.ReadLine();

        if (confirmacion.ToLower() == "s")
        {
            
            Pago pago = new Pago
            {
                Pedido = pedido,
                TarjetaNumero = IngresarDato("Ingrese el número de tarjeta: "),
                TarjetaVencimiento = DateTime.Parse(IngresarDato("Ingrese la fecha de vencimiento (MM/AAAA): ")),
                TarjetaCodigoSeguridad = IngresarDato("Ingrese el código de seguridad: ")
            };

            pago.ProcesarPago();

            // Descontar stock
            foreach (var detalle in pedido.Detalles)
            {
                detalle.Libro.DescontarStock(detalle.Cantidad);
            }

            Console.WriteLine("¡Gracias por su compra!");
        }
        else
        {
            Console.WriteLine("Compra cancelada.");
        }
    }


    static Usuario Registrar()
    {
        Console.WriteLine("\n--- Registro de Usuario ---");

        Console.Write("Ingrese su nombre: ");
        string nombre = Console.ReadLine();

        Console.Write("Ingrese su email: ");
        string email = Console.ReadLine();

        if (usuariosRegistrados.Exists(u => u.Email == email))
        {
            Console.WriteLine("Este email ya está registrado.");
            return null;
        }

        Console.Write("Ingrese una contraseña: ");
        string contrasena = Console.ReadLine();

        Usuario nuevoUsuario = new Usuario
        {
            Id = usuariosRegistrados.Count + 1,
            Nombre = nombre,
            Email = email,
            Contrasena = contrasena
        };

        usuariosRegistrados.Add(nuevoUsuario);
        Console.WriteLine("Registro exitoso.");
        return nuevoUsuario;
    }

   
    static Usuario Login()
    {
        Console.WriteLine("\n--- Iniciar Sesión ---");

        Console.Write("Ingrese su email: ");
        string email = Console.ReadLine();

        Console.Write("Ingrese su contraseña: ");
        string contrasena = Console.ReadLine();

        Usuario usuario = usuariosRegistrados.Find(u => u.Autenticar(email, contrasena));

        if (usuario == null)
        {
            Console.WriteLine("Email o contraseña incorrectos. Inténtelo de nuevo.");
        }
        else
        {
            Console.WriteLine("¡Inicio de sesión exitoso!");
        }

        return usuario;
    }

    static List<Libro> InicializarCatalogo()
    {
        return new List<Libro>
        {
            new Libro { Id = 1, Titulo = "C# Avanzado", Autor = "Autor 1", Precio = 100, Stock = 10 },
            new Libro { Id = 2, Titulo = "ASP.NET Core", Autor = "Autor 2", Precio = 120, Stock = 5 },
            new Libro { Id = 3, Titulo = "Patrones de Diseño", Autor = "Autor 3", Precio = 80, Stock = 7 }
        };
    }

   
    static void MostrarCatalogo(List<Libro> catalogo)
    {
        Console.WriteLine("\nCatálogo de libros disponibles:");
        foreach (var libro in catalogo)
        {
            Console.WriteLine($"ID: {libro.Id} - {libro.Titulo} - Precio: ${libro.Precio} - Stock: {libro.Stock}");
        }
    }


    static string IngresarDato(string mensaje)
    {
        Console.Write(mensaje);
        return Console.ReadLine();
    }
}
