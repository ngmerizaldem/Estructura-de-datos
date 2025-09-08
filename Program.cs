using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

// Clase Jugador
public class Jugador
{
    public string Id { get; set; }
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public string Posicion { get; set; }

    public override bool Equals(object obj)
    {
        if (obj is Jugador other) return Id == other.Id;
        return false;
    }
    public override int GetHashCode() => Id.GetHashCode();

    public override string ToString() => $"{Nombre} (ID: {Id}) - {Posicion}, {Edad} años";
}

// Clase Equipo
public class Equipo
{
    public string Nombre { get; set; }
    public HashSet<Jugador> Jugadores { get; set; } = new HashSet<Jugador>();

    public override string ToString() => $"{Nombre} (Jugadores: {Jugadores.Count})";
}

// Clase Torneo
public class Torneo
{
    public Dictionary<string, Equipo> Equipos { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    public HashSet<Jugador> JugadoresGlobales { get; set; } = new();

    public bool AgregarEquipo(string nombre)
    {
        if (Equipos.ContainsKey(nombre)) return false;
        Equipos[nombre] = new Equipo { Nombre = nombre };
        return true;
    }

    public bool AgregarJugador(string equipoNombre, Jugador jugador)
    {
        if (!Equipos.ContainsKey(equipoNombre)) return false;

        if (JugadoresGlobales.TryGetValue(jugador, out Jugador existente))
        {
            jugador = existente;
        }
        else
        {
            JugadoresGlobales.Add(jugador);
        }

        return Equipos[equipoNombre].Jugadores.Add(jugador);
    }

    public Jugador BuscarJugadorPorId(string id) =>
        JugadoresGlobales.FirstOrDefault(j => j.Id == id);

    public string ObtenerEquipoDeJugador(string id)
    {
        foreach (var kv in Equipos)
        {
            if (kv.Value.Jugadores.Any(j => j.Id == id)) return kv.Key;
        }
        return null;
    }

    public IEnumerable<Jugador> ListarJugadoresEquipo(string equipoNombre)
    {
        if (!Equipos.ContainsKey(equipoNombre)) return Enumerable.Empty<Jugador>();
        return Equipos[equipoNombre].Jugadores.OrderBy(j => j.Nombre);
    }

    public IEnumerable<Equipo> ListarEquipos() => Equipos.Values.OrderBy(e => e.Nombre);

    public Equipo EquipoConMasJugadores() =>
        Equipos.Values.OrderByDescending(e => e.Jugadores.Count).FirstOrDefault();

    // Persistencia en JSON
    public void Guardar(string ruta)
    {
        var opciones = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(ruta, JsonSerializer.Serialize(this, opciones));
    }

    public static Torneo Cargar(string ruta)
    {
        if (!File.Exists(ruta)) return new Torneo();
        var texto = File.ReadAllText(ruta);
        return JsonSerializer.Deserialize<Torneo>(texto) ?? new Torneo();
    }
}

// Programa principal
class Program
{
    static string DATA_FILE = "data.json";

    static void Main(string[] args)
    {
        var torneo = Torneo.Cargar(DATA_FILE);
        Console.WriteLine("Registro de Torneo de Fútbol - Consola");
        bool salir = false;

        while (!salir)
        {
            MostrarMenu();
            var opt = Console.ReadLine();
            switch (opt)
            {
                case "1": CrearEquipo(torneo); break;
                case "2": CrearJugadorYAgregar(torneo); break;
                case "3": ListarEquipos(torneo); break;
                case "4": ListarJugadoresEquipo(torneo); break;
                case "5": BuscarJugador(torneo); break;
                case "6": MostrarEquipoConMasJugadores(torneo); break;
                case "7": torneo.Guardar(DATA_FILE); Console.WriteLine("Datos guardados."); break;
                case "0": torneo.Guardar(DATA_FILE); salir = true; break;
                default: Console.WriteLine("Opción no válida."); break;
            }
            Console.WriteLine();
        }
    }

    static void MostrarMenu()
    {
        Console.WriteLine("1) Crear equipo");
        Console.WriteLine("2) Crear jugador y agregar a equipo");
        Console.WriteLine("3) Listar equipos");
        Console.WriteLine("4) Listar jugadores de un equipo");
        Console.WriteLine("5) Buscar jugador por ID");
        Console.WriteLine("6) Equipo con más jugadores");
        Console.WriteLine("7) Guardar ahora");
        Console.WriteLine("0) Salir (guarda automáticamente)");
        Console.Write("Seleccione: ");
    }

    static void CrearEquipo(Torneo t)
    {
        Console.Write("Nombre del equipo: ");
        var nombre = Console.ReadLine().Trim();
        if (string.IsNullOrEmpty(nombre)) { Console.WriteLine("Nombre inválido"); return; }
        if (t.AgregarEquipo(nombre)) Console.WriteLine("Equipo agregado.");
        else Console.WriteLine("El equipo ya existe.");
    }

    static void CrearJugadorYAgregar(Torneo t)
    {
        Console.Write("ID (documento): "); var id = Console.ReadLine().Trim();
        Console.Write("Nombre: "); var nombre = Console.ReadLine().Trim();
        Console.Write("Edad: "); if (!int.TryParse(Console.ReadLine(), out int edad)) edad = 0;
        Console.Write("Posición: "); var pos = Console.ReadLine().Trim();
        Console.Write("Equipo al que agregar: "); var equipo = Console.ReadLine().Trim();

        var jugador = new Jugador { Id = id, Nombre = nombre, Edad = edad, Posicion = pos };
        if (!t.Equipos.ContainsKey(equipo))
        {
            Console.WriteLine("El equipo no existe. Cree primero el equipo.");
            return;
        }
        bool agregado = t.AgregarJugador(equipo, jugador);
        Console.WriteLine(agregado ? "Jugador agregado al equipo." : "El jugador ya está en ese equipo.");
    }

    static void ListarEquipos(Torneo t)
    {
        Console.WriteLine("Equipos registrados:");
        foreach (var e in t.ListarEquipos()) Console.WriteLine(e);
    }

    static void ListarJugadoresEquipo(Torneo t)
    {
        Console.Write("Nombre del equipo: "); var equipo = Console.ReadLine().Trim();
        var lista = t.ListarJugadoresEquipo(equipo);
        if (!lista.Any()) { Console.WriteLine("Equipo no existe o no tiene jugadores."); return; }
        Console.WriteLine($"Jugadores en {equipo}:");
        foreach (var j in lista) Console.WriteLine(j);
    }

    static void BuscarJugador(Torneo t)
    {
        Console.Write("ID del jugador: "); var id = Console.ReadLine().Trim();
        var j = t.BuscarJugadorPorId(id);
        if (j == null) { Console.WriteLine("Jugador no encontrado."); return; }
        Console.WriteLine(j);
        var equipo = t.ObtenerEquipoDeJugador(id);
        Console.WriteLine(equipo != null ? $"Pertenece al equipo: {equipo}" : "No está asignado a ningún equipo");
    }

    static void MostrarEquipoConMasJugadores(Torneo t)
    {
        var e = t.EquipoConMasJugadores();
        Console.WriteLine(e != null ? $"Equipo con más jugadores: {e.Nombre} ({e.Jugadores.Count})" : "No hay equipos");
    }
}
