while (true)
{
    Console.WriteLine("\n--- Try/Catch Challenge ---");
    Console.WriteLine("1. Testa FormatException");
    Console.WriteLine("2. Testa FileNotFoundException");
    Console.WriteLine("3. Testa ett annat fel");
    Console.WriteLine("4. Avsluta");

    Console.Write("Välj ett alternativ: ");
    string val = Console.ReadLine() ?? "";

    if (val == "4")
    {
        Console.WriteLine("Programmet avslutas.");
        break;
    }

    try
    {
        if (val == "1")
        {
            Console.Write("Skriv ett nummer: ");
            int nummer = int.Parse(Console.ReadLine() ?? "");

            Console.WriteLine($"Du skrev: {nummer}");
        }
        else if (val == "2")
        {
            string text = File.ReadAllText("finnsinte.txt");

            Console.WriteLine(text);
        }
        else if (val == "3")
        {
            int[] nummerLista = { 10, 20, 30 };

            Console.WriteLine(nummerLista[5]);
        }
        else
        {
            Console.WriteLine("Ogiltigt val!");
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Fel: Du måste skriva ett nummer!");
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine("Fel: Filen kunde inte hittas!");
    }
    catch (Exception)
    {
        Console.WriteLine("Fel: Ett oväntat fel inträffade!");
    }
}
