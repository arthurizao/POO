// Desafio 1 [TESTE TECNICO GFT]
// Arthur Areias Mariano

using System;

class Program
{
    static void Main()
    {
        Console.WriteLine(" - REUNIÃO SEM FIM -\n");
        
        Console.Write("Hora de início (hh mm): ");
        string[] inicioInput = Console.ReadLine().Split(' ');
        int horaInicio = int.Parse(inicioInput[0]);
        int minutoInicio = int.Parse(inicioInput[1]);

        Console.Write("Hora atual (hh mm): ");
        string[] atualInput = Console.ReadLine().Split(' ');
        int horaAtual = int.Parse(atualInput[0]);
        int minutoAtual = int.Parse(atualInput[1]);
        
        int minutosInicio = horaInicio * 60 + minutoInicio;
        int minutosAtual = horaAtual * 60 + minutoAtual;
        
        if (minutosAtual < minutosInicio)
        {
            minutosAtual += 24 * 60; 
        }
        
        int duracaoMinutos = minutosAtual - minutosInicio;
        int horas = duracaoMinutos / 60;
        int minutos = duracaoMinutos % 60;
        
        Console.WriteLine($"Tempo total: {horas}h{minutos:D2}min");
    }
}
