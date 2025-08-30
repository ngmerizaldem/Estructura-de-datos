using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
  // Diccionario base español -> inglés
static Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
{
    {"tiempo", "time"},
    {"persona", "person"},
    {"año", "year"},
    {"camino", "way"},
    {"día", "day"},
    {"cosa", "thing"},
    {"hombre", "man"},
    {"mundo", "world"},
    {"vida", "life"},
    {"mano", "hand"},
    {"parte", "part"},
    {"niño", "child"},
    {"ojo", "eye"},
    {"mujer", "woman"},
    {"lugar", "place"},
    {"trabajo", "work"},
    {"semana", "week"},
    {"caso", "case"},
    {"punto", "point"},
    {"gobierno", "government"},
    {"empresa", "company"}
};


    static void Main()
    {
        int option;
        do
        {
            Console.Clear();
            Console.WriteLine("==================== MENÚ ====================");
            Console.WriteLine("1. Traducir una frase");
            Console.WriteLine("2. Agregar palabras al diccionario");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");

            if (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Opción inválida, presione una tecla para continuar...");
                Console.ReadKey();
                continue;
            }

            switch (option)
            {
                case 1:
                    TranslateSentence();
                    break;
                case 2:
                    AddWord();
                    break;
                case 0:
                    Console.WriteLine("¡Gracias por usar el traductor!");
                    break;
                default:
                    Console.WriteLine("Opción inválida. Intente nuevamente.");
                    break;
            }

            if (option != 0)
            {
                Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
                Console.ReadKey();
            }

        } while (option != 0);
    }

    static void TranslateSentence()
    {
        Console.Clear();
        Console.WriteLine("===== Traducir una frase =====");
        Console.Write("Ingrese la frase: ");
        string sentence = Console.ReadLine();

        string[] words = sentence.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            string cleanWord = new string(words[i].Where(char.IsLetter).ToArray());
            string punctuation = new string(words[i].Where(c => !char.IsLetter(c)).ToArray());

            if (dictionary.ContainsKey(cleanWord.ToLower()))
            {
                words[i] = dictionary[cleanWord.ToLower()] + punctuation;
            }
        }

        Console.WriteLine("\nTraducción: " + string.Join(" ", words));
    }

    static void AddWord()
    {
        Console.Clear();
        Console.WriteLine("===== Agregar palabras al diccionario =====");
        Console.Write("Ingrese la palabra en inglés: ");
        string englishWord = Console.ReadLine().Trim().ToLower();

        if (dictionary.ContainsKey(englishWord))
        {
            Console.WriteLine("La palabra ya existe en el diccionario.");
            return;
        }

        Console.Write("Ingrese su traducción en español: ");
        string spanishWord = Console.ReadLine().Trim().ToLower();

        dictionary.Add(englishWord, spanishWord);
        Console.WriteLine("¡Palabra agregada exitosamente!");
    }
}
