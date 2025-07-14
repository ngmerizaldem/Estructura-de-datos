using System;
using System.Collections.Generic;

class TorresDeHanoi
{
    class Torre
    {
        public Stack<int> discos;
        public string nombre;

        public Torre(string nombre)
        {
            this.nombre = nombre;
            discos = new Stack<int>();
        }

        public void Mostrar()
        {
            Console.Write(nombre + ": ");
            foreach (int disco in discos)
                Console.Write(disco + " ");
            Console.WriteLine();
        }
    }

    public static void MoverDiscos(int n, Torre origen, Torre destino, Torre auxiliar)
    {
        if (n == 1)
        {
            int disco = origen.discos.Pop();
            destino.discos.Push(disco);
            Console.WriteLine($"Mover disco {disco} de {origen.nombre} a {destino.nombre}");
            MostrarTorres(origen, destino, auxiliar);
            return;
        }

        MoverDiscos(n - 1, origen, auxiliar, destino);
        MoverDiscos(1, origen, destino, auxiliar);
        MoverDiscos(n - 1, auxiliar, destino, origen);
    }

    public static void MostrarTorres(Torre t1, Torre t2, Torre t3)
    {
        t1.Mostrar();
        t2.Mostrar();
        t3.Mostrar();
        Console.WriteLine(new string('-', 30));
    }

    static void Main()
    {
        int numDiscos = 3; // Puedes cambiar el número de discos

        Torre torreA = new Torre("A");
        Torre torreB = new Torre("B");
        Torre torreC = new Torre("C");

        for (int i = numDiscos; i >= 1; i--)
            torreA.discos.Push(i);

        Console.WriteLine("Estado inicial:");
        MostrarTorres(torreA, torreB, torreC);

        MoverDiscos(numDiscos, torreA, torreC, torreB);
    }
}
