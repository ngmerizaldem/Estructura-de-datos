using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // 1. Crear conjunto de 500 ciudadanos
        HashSet<string> ciudadanos = new HashSet<string>();
        for (int i = 1; i <= 500; i++)
        {
            ciudadanos.Add($"Ciudadano {i}");
        }

        // 2. Crear conjuntos de vacunados
        HashSet<string> vacunadosPfizer = new HashSet<string>();
        HashSet<string> vacunadosAstraZeneca = new HashSet<string>();

        Random random = new Random();

        // 75 vacunados con Pfizer
        while (vacunadosPfizer.Count < 75)
        {
            vacunadosPfizer.Add($"Ciudadano {random.Next(1, 501)}");
        }

        // 75 vacunados con AstraZeneca
        while (vacunadosAstraZeneca.Count < 75)
        {
            vacunadosAstraZeneca.Add($"Ciudadano {random.Next(1, 501)}");
        }

        // 3. Operaciones de conjuntos
        // Ciudadanos no vacunados
        var noVacunados = new HashSet<string>(ciudadanos);
        noVacunados.ExceptWith(vacunadosPfizer);
        noVacunados.ExceptWith(vacunadosAstraZeneca);

        // Ciudadanos con ambas dosis
        var ambasDosis = new HashSet<string>(vacunadosPfizer);
        ambasDosis.IntersectWith(vacunadosAstraZeneca);

        // Solo Pfizer
        var soloPfizer = new HashSet<string>(vacunadosPfizer);
        soloPfizer.ExceptWith(vacunadosAstraZeneca);

        // Solo AstraZeneca
        var soloAstraZeneca = new HashSet<string>(vacunadosAstraZeneca);
        soloAstraZeneca.ExceptWith(vacunadosPfizer);

        // 4. Resultados
        Console.WriteLine("=== RESULTADOS DE VACUNACIÓN COVID-19 ===\n");
        Console.WriteLine($"Total de ciudadanos: {ciudadanos.Count}");
        Console.WriteLine($"No vacunados: {noVacunados.Count}");
        Console.WriteLine($"Con ambas dosis: {ambasDosis.Count}");
        Console.WriteLine($"Solo Pfizer: {soloPfizer.Count}");
        Console.WriteLine($"Solo AstraZeneca: {soloAstraZeneca.Count}\n");
    }
}
