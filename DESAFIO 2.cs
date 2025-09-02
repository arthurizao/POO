// Desafio 2 [TESTE TECNICO GFT]
// Arthur Areias Mariano

using System;

class Program
{
    static void Main()
    {
        decimal precoCafe = 2.50m;
        decimal totalInserido = 0m;
        string moedas = "";
        
        Console.WriteLine(" - TROCO DA MÁQUINA DE CAFÉ -\n");
        
        Console.Write("Digite as moedas separadas por espaço (ex: 2.00 1.00 ou 2 1): ");
        string[] moedasInput = Console.ReadLine().Split(' ');
        
        foreach (string moedaStr in moedasInput)
        {
            decimal moeda = decimal.Parse(moedaStr);
            totalInserido += moeda;
            
            if (moedas == "")
                moedas = moeda.ToString("F2");
            else
                moedas += " + " + moeda.ToString("F2");
        }
        
        Console.WriteLine($"Moedas inseridas: {moedas}");
        Console.WriteLine($"Valor total: R$ {totalInserido:F2}");
        
        if (totalInserido > precoCafe)
        {
            decimal troco = totalInserido - precoCafe;
            Console.WriteLine($"Troco: R$ {troco:F2}");
        }
        else if (totalInserido == precoCafe)
        {
            Console.WriteLine("Valor exato - sem troco.");
        }
        else
        {
            decimal falta = precoCafe - totalInserido;
            Console.WriteLine($"Ainda falta: R$ {falta:F2}");
        }
    }
}
