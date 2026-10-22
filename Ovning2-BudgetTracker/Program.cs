using System.Globalization;
using System.Text.Json;

string filnamn = "budget.json";

List<Expense> utgifter = new List<Expense>();

// Läs in tidigare utgifter
try
{
    if (File.Exists(filnamn))
    {
        string json = File.ReadAllText(filnamn);

        if (!string.IsNullOrWhiteSpace(json))
        {
            utgifter = JsonSerializer.Deserialize<List<Expense>>(json)
                       ?? new List<Expense>();

            Console.WriteLine("Tidigare utgifter har lästs in!");
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

// Skapa meny
while (true)
{
    Console.WriteLine("\n--- Budget Tracker ---");
    Console.WriteLine("1. Lägg till utgift");
    Console.WriteLine("2. Visa alla utgifter");
    Console.WriteLine("3. Avsluta");

    Console.Write("Välj ett alternativ: ");
    string val = Console.ReadLine() ?? "";

    if (val == "1")
    {
        Console.Write("Ange kategori: ");
        string kategori = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(kategori))
        {
            Console.WriteLine("Fel: Kategori får inte vara tom!");
            continue;
        }

        Console.Write("Ange belopp: ");
        string beloppText = Console.ReadLine() ?? "";

        if (!decimal.TryParse(
            beloppText,
            NumberStyles.Number,
            CultureInfo.GetCultureInfo("sv-SE"),
            out decimal belopp) || belopp <= 0)
        {
            Console.WriteLine("Fel: Ange ett giltigt positivt belopp!");
            continue;
        }

        Console.Write("Ange datum (yyyy-MM-dd): ");
        string datumText = Console.ReadLine() ?? "";

        if (!DateTime.TryParseExact(
            datumText,
            "yyyy-MM-dd",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out DateTime datum))
        {
            Console.WriteLine("Fel: Ogiltigt datum!");
            continue;
        }

        Expense utgift = new Expense
        {
            Kategori = kategori,
            Belopp = belopp,
            Datum = datum
        };

        utgifter.Add(utgift);

        // Spara alla utgifter till JSON
        try
        {
            string json = JsonSerializer.Serialize(
                utgifter,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(filnamn, json);

            Console.WriteLine("Utgiften har sparats i budget.json!");
        }
        catch (IOException)
        {
            Console.WriteLine("Fel: Kunde inte spara filen!");
        }
    }
    else if (val == "2")
    {
        Console.WriteLine("\n--- Alla utgifter ---");

        if (utgifter.Count == 0)
        {
            Console.WriteLine("Inga utgifter registrerade.");
        }
        else
        {
            foreach (Expense utgift in utgifter)
            {
                Console.WriteLine(
                    $"{utgift.Kategori}: {utgift.Belopp} kr - {utgift.Datum:yyyy-MM-dd}"
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