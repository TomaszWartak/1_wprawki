namespace CalcTest;

using System;
using Xunit.Abstractions;

public class ConsoleOutputHelper : ITestOutputHelper
{
    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }

    /**
     * metoda, w której wołasz metodę helpera -> mojaKlasa.PrzykladowaMetoda(42, "Przykładowy tekst");
     * użycie w metodzie helpera ->  output.WriteLine("Wywołano metodę z numerem: {0}, i nazwą: {1}", 42, "Przykładowy tekst");
     */
    public void WriteLine(string format, params object[] args)
    {
        Console.WriteLine(format, args);
    }
}
