﻿namespace IAEA_CS_PoC_Consola
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("PoC - Reactores Nucleares Investigación \n");

            Console.WriteLine("Ejecutando PoC en Mongo...");
            PoC_Mongo.Ejecuta_PoC();
        }
    }
}