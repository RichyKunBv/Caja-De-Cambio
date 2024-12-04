using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CajaDeCambio
{
    class Denominacion
    {
        public decimal Valor { get; }
        public int Cantidad { get; set; }

        public bool EsBillete => Valor >= 20; // Determina si es un billete o moneda.

        public Denominacion(decimal valor, int cantidad)
        {
            Valor = valor;
            Cantidad = cantidad;
        }
    }

    class SistemaCambio
    {
        private static readonly Dictionary<int, string> Errores = new Dictionary<int, string>
        {
            { 1, "Denominación incorrecta." },
            { 2, "No hay suficientes billetes y/o monedas para dar cambio de la denominación ingresada." },
            { 3, "La moneda de $ 1.00 no ofrece cambio." },
            { 4, "La opción ingresada es incorrecta." },
            { 5, "Error desconocido en el proceso de cambio." }
        };

        private List<Denominacion> denominaciones;

        // Ruta absoluta para el archivo
        private readonly string RutaArchivo = Path.Combine(Directory.GetCurrentDirectory(), "inventario.txt");

        public SistemaCambio()
        {
            denominaciones = new List<Denominacion>
            {
                new Denominacion(1000, 0),
                new Denominacion(500, 5),
                new Denominacion(200, 7),
                new Denominacion(100, 9),
                new Denominacion(50, 11),
                new Denominacion(20, 13),
                new Denominacion(10, 17),
                new Denominacion(5, 23),
                new Denominacion(2, 29),
                new Denominacion(1, 31)
            };
            ActualizarArchivo();
        }

        public void DarCambio(decimal monto)
        {
            if (monto <= 0)
            {
                Console.WriteLine(Errores[4]); // "La opción ingresada es incorrecta."
                return;
            }

            if (monto == 1)
            {
                Console.WriteLine(Errores[3]); // "La moneda de $ 1.00 no ofrece cambio."
                return;
            }

            if (monto != Math.Floor(monto))
            {
                Console.WriteLine(Errores[5]); // "Error desconocido en el proceso de cambio."
                return;
            }

            var denomOrdenadas = denominaciones.OrderByDescending(d => d.Valor).ToList();
            var cambio = new List<(decimal Valor, int Cantidad)>();
            decimal montoRestante = monto;

            foreach (var denom in denomOrdenadas)
            {
                if (montoRestante == 0)
                    break;

                int cantidadNecesaria = Math.Min((int)(montoRestante / denom.Valor), denom.Cantidad);

                if (cantidadNecesaria > 0)
                {
                    cambio.Add((denom.Valor, cantidadNecesaria));
                    montoRestante -= cantidadNecesaria * denom.Valor;
                    denom.Cantidad -= cantidadNecesaria;
                }
            }

            if (montoRestante > 0)
            {
                foreach (var item in cambio)
                {
                    var denom = denominaciones.First(d => d.Valor == item.Valor);
                    denom.Cantidad += item.Cantidad;
                }
                Console.WriteLine(Errores[2]); // "No hay suficientes billetes y/o monedas para dar cambio de la denominación ingresada."
            }
            else
            {
                Console.WriteLine($"Cambio entregado para ${monto}:");
                foreach (var item in cambio)
                {
                    string tipo = item.Valor >= 20 ? "Billete" : "Moneda";
                    Console.WriteLine($"{tipo}: ${item.Valor}, Cantidad: {item.Cantidad}");
                }
                ActualizarArchivo();
            }
        }

        public void MostrarInventario()
        {
            Console.WriteLine("Estado actual de la caja:");
            foreach (var denom in denominaciones)
            {
                string tipo = denom.Valor >= 20 ? "Billete" : "Moneda";
                Console.WriteLine($"{tipo}: ${denom.Valor}, Cantidad: {denom.Cantidad}");
            }
            ActualizarArchivo();
        }

        private void ActualizarArchivo()
        {
            try
            {
                using (var writer = new StreamWriter(RutaArchivo))
                {
                    foreach (var denom in denominaciones)
                    {
                        string tipo = denom.EsBillete ? "Billete" : "Moneda";
                        writer.WriteLine($"{tipo}: ${denom.Valor}, Cantidad: {denom.Cantidad}");
                    }
                }
                Console.WriteLine($"Inventario actualizado en {RutaArchivo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al escribir el archivo: {ex.Message}");
            }
        }

        public void Ejecutar()
        {
            while (true)
            {
                Console.WriteLine("\nOpciones:");
                Console.WriteLine("-1. Mostrar estado de la caja");
                Console.WriteLine("-2. Salir");
                Console.WriteLine(" 3. Solicitar cambio");

                Console.Write("Seleccione una opción: ");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int opcion))
                {
                    Console.WriteLine(Errores[4]);
                    continue;
                }

                switch (opcion)
                {
                    case -1:
                        MostrarInventario();
                        break;
                    case -2:
                        ActualizarArchivo();
                        Console.WriteLine("Saliendo del sistema...");
                        return;
                    case 3:
                        Console.Write("Ingrese el monto para cambio: ");
                        string montoInput = Console.ReadLine();
                        if (decimal.TryParse(montoInput, out decimal monto))
                        {
                            DarCambio(monto);
                        }
                        else
                        {
                            Console.WriteLine(Errores[4]);
                        }
                        break;
                    default:
                        Console.WriteLine(Errores[4]);
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            SistemaCambio sistema = new SistemaCambio();
            sistema.Ejecutar();
        }
    }
}
