using System;
using System.Collections.Generic;

class ParentesisBalanceados
{
    public static bool EstaBalanceado(string expresion)
    {
        Stack<char> pila = new Stack<char>();

        foreach (char c in expresion)
        {
            if (c == '(' || c == '{' || c == '[')
            {
                pila.Push(c);
            }
            else if (c == ')' || c == '}' || c == ']')
            {
                if (pila.Count == 0) return false;

                char ultimoAbierto = pila.Pop();

                if (!Corresponde(ultimoAbierto, c))
                    return false;
            }
        }
        return pila.Count == 0;
    }

    private static bool Corresponde(char abierto, char cerrado)
    {
        return (abierto == '(' && cerrado == ')') ||
               (abierto == '{' && cerrado == '}') ||
               (abierto == '[' && cerrado == ']');
    }

    static void Main()
    {
        string expresion = "{7 + (8 * 5) - [(9 - 7) + (4 + 1)]}";

        if (EstaBalanceado(expresion))
            Console.WriteLine("Fórmula balanceada.");
        else
            Console.WriteLine("Fórmula NO balanceada.");
    }
}
