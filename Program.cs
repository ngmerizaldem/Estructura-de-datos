using System;

namespace RegistroEstudiante
{
    class Estudiante
    {
        // Atributos del estudiante
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string[] Telefonos { get; set; }

        // Constructor
        public Estudiante(int id, string nombres, string apellidos, string direccion, string[] telefonos)
        {
            Id = id;
            Nombres = nombres;
            Apellidos = apellidos;
            Direccion = direccion;
            Telefonos = telefonos;
        }

        // Método para mostrar la información
        public void MostrarInformacion()
        {
            Console.WriteLine("=== Registro de Estudiante ===");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Nombres: {Nombres}");
            Console.WriteLine($"Apellidos: {Apellidos}");
            Console.WriteLine($"Dirección: {Direccion}");
            Console.WriteLine("Teléfonos:");
            for (int i = 0; i < Telefonos.Length; i++)
            {
                Console.WriteLine($"  Teléfono {i + 1}: {Telefonos[i]}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Crear un array de teléfonos
            string[] telefonos = { "0991234567", "022345678", "0969876543" };

            // Crear un objeto de estudiante
            Estudiante estudiante = new Estudiante(
                1,
                "Neila",
                "Morales",
                "Av. Siempre Viva 742",
                telefonos
            );

            // Mostrar la información del estudiante
            estudiante.MostrarInformacion();
        }
    }
}

