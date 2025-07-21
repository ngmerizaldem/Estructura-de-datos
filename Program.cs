using System;
using System.Collections.Generic;

namespace ParqueAtracciones
{
    class Persona
    {
        public string Nombre { get; set; }

        public Persona(string nombre)
        {
            Nombre = nombre;
        }

        public override string ToString()
        {
            return $"Pasajero: {Nombre}";
        }
    }

    class Atraccion
    {
        private Queue<Persona> colaEspera = new Queue<Persona>();
        private List<Persona> asientosAsignados = new List<Persona>();
        private const int MAX_ASIENTOS = 30;

        public void RegistrarPersona(string nombre)
        {
            if (asientosAsignados.Count >= MAX_ASIENTOS)
            {
                Console.WriteLine("Ya no hay asientos disponibles.");
                return;
            }

            var persona = new Persona(nombre);
            colaEspera.Enqueue(persona);
            Console.WriteLine($"{nombre} ha sido registrado en la cola.");
        }

        public void AsignarAsientos()
        {
            while (colaEspera.Count > 0 && asientosAsignados.Count < MAX_ASIENTOS)
            {
                var persona = colaEspera.Dequeue();
                asientosAsignados.Add(persona);
                Console.WriteLine($"{persona.Nombre} ha subido a la atracción.");
            }
        }

        public void MostrarReporte()
        {
            Console.WriteLine("\n--- Reporte de Atracción ---");
            Console.WriteLine($"Asientos asignados ({asientosAsignados.Count}/{MAX_ASIENTOS}):");

            foreach (var persona in asientosAsignados)
            {
                Console.WriteLine(persona);
            }

            Console.WriteLine($"\nPersonas en espera: {colaEspera.Count}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Atraccion atraccion = new Atraccion();
            string opcion;

            do
            {
                Console.WriteLine("\n1. Registrar persona");
                Console.WriteLine("2. Asignar asientos");
                Console.WriteLine("3. Mostrar reporte");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opción: ");
                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("Ingrese nombre de la persona: ");
                        string nombre = Console.ReadLine();
                        atraccion.RegistrarPersona(nombre);
                        break;

                    case "2":
                        atraccion.AsignarAsientos();
                        break;

                    case "3":
                        atraccion.MostrarReporte();
                        break;

                    case "4":
                        Console.WriteLine("Saliendo...");
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

            } while (opcion != "4");
        }
    }
}
