using System;
using System.Collections.Generic;

// Clase que representa un paciente
public class Paciente
{
    public string Nombre { get; set; }
    public string Cedula { get; set; }

    public Paciente(string nombre, string cedula)
    {
        Nombre = nombre;
        Cedula = cedula;
    }
}

// Clase que representa un turno
public class Turno
{
    public Paciente Paciente { get; set; }
    public DateTime FechaHora { get; set; }
    public string Medico { get; set; }

    public Turno(Paciente paciente, DateTime fechaHora, string medico)
    {
        Paciente = paciente;
        FechaHora = fechaHora;
        Medico = medico;
    }

    // Método para mostrar datos del turno
    public void MostrarInfo()
    {
        Console.WriteLine($"Paciente: {Paciente.Nombre} (Cédula: {Paciente.Cedula})");
        Console.WriteLine($"Fecha y Hora: {FechaHora}");
        Console.WriteLine($"Médico: {Medico}");
        Console.WriteLine("-----------------------------");
    }
}

// Clase Agenda que maneja los turnos
public class Agenda
{
    private List<Turno> turnos; // lista dinámica de turnos

    public Agenda()
    {
        turnos = new List<Turno>();
    }

    // Agregar un nuevo turno
    public void AgregarTurno(Turno turno)
    {
        turnos.Add(turno);
        Console.WriteLine("Turno agregado exitosamente.\n");
    }

    // Mostrar todos los turnos
    public void MostrarTurnos()
    {
        if (turnos.Count == 0)
        {
            Console.WriteLine("No hay turnos registrados.\n");
            return;
        }

        Console.WriteLine("Lista de turnos agendados:");
        Console.WriteLine("==========================");

        foreach (var turno in turnos)
        {
            turno.MostrarInfo();
        }
    }

    // Buscar turnos por cédula del paciente
    public void BuscarTurnoPorCedula(string cedula)
    {
        var encontrados = turnos.FindAll(t => t.Paciente.Cedula == cedula);

        if (encontrados.Count == 0)
        {
            Console.WriteLine($"No se encontraron turnos para la cédula {cedula}.\n");
            return;
        }

        Console.WriteLine($"Turnos para la cédula {cedula}:");
        Console.WriteLine("===============================");

        foreach (var turno in encontrados)
        {
            turno.MostrarInfo();
        }
    }
}

// Clase principal con el menú para interacción
class Program
{
    static void Main(string[] args)
    {
        Agenda agenda = new Agenda();
        bool continuar = true;

        while (continuar)
        {
            Console.WriteLine("=== Agenda de Turnos de Clínica ===");
            Console.WriteLine("1. Agregar turno");
            Console.WriteLine("2. Mostrar todos los turnos");
            Console.WriteLine("3. Buscar turno por cédula");
            Console.WriteLine("4. Salir");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    AgregarTurno(agenda);
                    break;
                case "2":
                    agenda.MostrarTurnos();
                    break;
                case "3":
                    Console.Write("Ingrese la cédula del paciente: ");
                    string cedula = Console.ReadLine();
                    agenda.BuscarTurnoPorCedula(cedula);
                    break;
                case "4":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Opción inválida, intente nuevamente.\n");
                    break;
            }
        }
    }

    // Método auxiliar para agregar turno solicitando datos por consola
    static void AgregarTurno(Agenda agenda)
    {
        Console.Write("Nombre del paciente: ");
        string nombre = Console.ReadLine();

        Console.Write("Cédula del paciente: ");
        string cedula = Console.ReadLine();

        Console.Write("Fecha y hora del turno (formato yyyy-MM-dd HH:mm): ");
        string fechaHoraStr = Console.ReadLine();

        DateTime fechaHora;
        if (!DateTime.TryParseExact(fechaHoraStr, "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out fechaHora))
        {
            Console.WriteLine("Fecha u hora inválida. Formato correcto: yyyy-MM-dd HH:mm\n");
            return;
        }

        Console.Write("Nombre del médico: ");
        string medico = Console.ReadLine();

        Paciente paciente = new Paciente(nombre, cedula);
        Turno turno = new Turno(paciente, fechaHora, medico);

        agenda.AgregarTurno(turno);
    }
}
