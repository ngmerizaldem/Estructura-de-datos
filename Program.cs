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
    public void Mostrar
