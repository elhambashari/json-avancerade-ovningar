using System.Text.Json;

string filnamn = "anvandare.json";

List<Anvandare> anvandareLista = new List<Anvandare>();

// Läs in sparade användare när programmet startar
try
{
    if (File.Exists(filnamn))
    {
        string json = File.ReadAllText(filnamn);

        if (!string.IsNullOrWhiteSpace(json))
        {
            anvandareLista =
                JsonSerializer.Deserialize<List<Anvandare>>(json)
                ?? new List<Anvandare>();

            Console.WriteLine("Sparade användare har lästs in!");
        }
    }
}
catch (JsonException)
{
    Console.WriteLine("Fel: JSON-filen innehåller ogiltig data!");
}
catch (IOException)
{
    Console.WriteLine("Fel: Kunde inte läsa filen!");
}
catch (UnauthorizedAccessException)
{
    Console.WriteLine("Fel: Ingen behörighet att läsa filen!");
}

// Visa meny
while (true)
{
    Console.WriteLine("\n--- Användarhanterare ---");
    Console.WriteLine("1. Registrera användare");
    Console.WriteLine("2. Visa alla användare");
    Console.WriteLine("3. Avsluta");

    Console.Write("Välj ett alternativ: ");
    string val = Console.ReadLine() ?? "";

    if (val == "1")
    {
        Console.Write("Ange namn: ");
        string namn = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(namn))
        {
            Console.WriteLine("Fel: Namnet får inte vara tomt!");
            continue;
        }

        Console.Write("Ange email: ");
        string email = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(email))
        {
            Console.WriteLine("Fel: Email får inte vara tom!");
            continue;
        }

        if (!email.Contains("@") || !email.Contains("."))
        {
            Console.WriteLine("Fel: Ogiltig email!");
            continue;
        }

        Console.Write("Ange ålder: ");
        string alderText = Console.ReadLine() ?? "";

        if (!int.TryParse(alderText, out int alder) || alder <= 0)
        {
            Console.WriteLine("Fel: Ange en giltig ålder!");
            continue;
        }

        Anvandare anvandare = new Anvandare
        {
            Namn = namn,
            Email = email,
            Alder = alder
        };

        anvandareLista.Add(anvandare);

        // Spara användarna till JSON
        try
        {
            string json = JsonSerializer.Serialize(
                anvandareLista,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(filnamn, json);

            Console.WriteLine("Användaren har sparats i JSON!");
        }
        catch (IOException)
        {
            Console.WriteLine("Fel: Kunde inte spara filen!");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Fel: Ingen behörighet att spara filen!");
        }
    }
    else if (val == "2")
    {
        Console.WriteLine("\n--- Alla användare ---");

        if (anvandareLista.Count == 0)
        {
            Console.WriteLine("Inga användare registrerade.");
        }
        else
        {
            foreach (Anvandare anvandare in anvandareLista)
            {
                Console.WriteLine(
                    $"Namn: {anvandare.Namn}, Email: {anvandare.Email}, Ålder: {anvandare.Alder}"
                );
            }
        }
    }
    else if (val == "3")
    {
        Console.WriteLine("Programmet avslutas.");
        break;
    }
    else
    {
        Console.WriteLine("Ogiltigt val!");
    }
}