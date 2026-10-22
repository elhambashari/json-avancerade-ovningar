using System.Text.Json;

// Anropa metoden
List<string> namnLista = LasJsonFil("namn.json");

// Visa resultatet
Console.WriteLine("--- Namnlista ---");

if (namnLista.Count == 0)
{
    Console.WriteLine("Listan är tom.");
}
else
{
    foreach (string namn in namnLista)
    {
        Console.WriteLine(namn);
    }
}

// Metod för att läsa JSON-filen
static List<string> LasJsonFil(string filnamn)
{
    try
    {
        string json = File.ReadAllText(filnamn);

        List<string> lista =
            JsonSerializer.Deserialize<List<string>>(json)
            ?? new List<string>();

        return lista;
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine("Fel: Filen finns inte!");
        return new List<string>();
    }
    catch (JsonException)
    {
        Console.WriteLine("Fel: Ogiltig JSON!");
        return new List<string>();
    }
    catch (IOException)
    {
        Console.WriteLine("Fel: Kunde inte läsa filen!");
        return new List<string>();
    }
}
