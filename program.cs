using System;
using System.Collections.Generic;  // Todos los using van primero

class Program
{
    static List<string> catalogo = new List<string>()
    {
        "National Geographic",
        "Time",
        "Scientific American",
        "Forbes",
        "Vogue",
        "The Economist",
        "Nature",
        "Popular Science",
        "Reader's Digest",
        "Wired"
    };

    static void Main()
    {
        bool salir = false;

        while (!salir)
        {
            Console.WriteLine("\n--- Catálogo de Revistas ---");
            Console.WriteLine("1. Buscar título (Iterativo)");
            Console.WriteLine("2. Buscar título (Recursivo)");
            Console.WriteLine("3. Mostrar catálogo");
            Console.WriteLine("4. Salir");
            Console.Write("Seleccione una opción: ");
            
            string opcion = Console.ReadLine();

            switch(opcion)
            {
                case "1":
                    BuscarIterativo();
                    break;
                case "2":
                    BuscarRecursivoMenu();
                    break;
                case "3":
                    MostrarCatalogo();
                    break;
                case "4":
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida, intente nuevamente.");
                    break;
            }
        }

        Console.WriteLine("Programa finalizado.");
    }

    static void BuscarIterativo()
    {
        Console.Write("Ingrese el título de la revista a buscar: ");
        string titulo = Console.ReadLine();
        bool encontrado = false;

        foreach (string revista in catalogo)
        {
            if (revista.Equals(titulo, StringComparison.OrdinalIgnoreCase))
            {
                encontrado = true;
                break;
            }
        }

        Console.WriteLine(encontrado ? "Encontrado" : "No encontrado");
    }

    static void BuscarRecursivoMenu()
    {
        Console.Write("Ingrese el título de la revista a buscar: ");
        string titulo = Console.ReadLine();
        bool encontrado = BuscarRecursivo(catalogo, titulo, 0);
        Console.WriteLine(encontrado ? "Encontrado" : "No encontrado");
    }

    static bool BuscarRecursivo(List<string> lista, string titulo, int indice)
    {
        if (indice >= lista.Count)
            return false;

        if (lista[indice].Equals(titulo, StringComparison.OrdinalIgnoreCase))
            return true;

        return BuscarRecursivo(lista, titulo, indice + 1);
    }

    static void MostrarCatalogo()
    {
        Console.WriteLine("\nCatálogo de Revistas:");
        foreach (string revista in catalogo)
        {
            Console.WriteLine("- " + revista);
        }
    }
}
