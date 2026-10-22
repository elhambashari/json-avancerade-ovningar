using System.Text.Json;

List<Student> studenter = new List<Student>();

string filnamn = "students.json";

// Läs in sparade studenter när programmet startar
try
{
    if (File.Exists(filnamn))
    {
        string json = File.ReadAllText(filnamn);

        studenter = JsonSerializer.Deserialize<List<Student>>(json)
                    ?? new List<Student>();

        Console.WriteLine("Studenter har lästs in från JSON!");
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

while (true)
{
    Console.WriteLine("\n--- Studentregister ---");
    Console.WriteLine("1. Lägg till student");
    Console.WriteLine("2. Visa alla studenter");
    Console.WriteLine("3. Avsluta");
    Console.WriteLine("4. Spara studenter till JSON");

    Console.Write("Välj ett alternativ: ");
    string val = Console.ReadLine() ?? "";

    if (val == "1")
    {
        Console.Write("Ange studentens namn: ");
        string namn = Console.ReadLine() ?? "";

        Console.Write("Ange studentens betyg: ");

        if (int.TryParse(Console.ReadLine(), out int betyg))
        {
            Student student = new Student
            {
                Name = namn,
                Grade = betyg
            };

            studenter.Add(student);

            Console.WriteLine("Studenten har lagts till!");
        }
        else
        {
            Console.WriteLine("Fel: Du måste ange ett nummer!");
        }
    }
    else if (val == "2")
    {
        Console.WriteLine("\n--- Alla studenter ---");

        foreach (Student student in studenter)
        {
            Console.WriteLine($"{student.Name}: {student.Grade}");
        }
    }
    else if (val == "3")
    {
        Console.WriteLine("Programmet avslutas.");
        break;
    }
    else if (val == "4")
    {
        try
        {
            string json = JsonSerializer.Serialize(
                studenter,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(filnamn, json);

            Console.WriteLine("Studenterna har sparats i students.json!");
        }
        catch (IOException)
        {
            Console.WriteLine("Fel: Kunde inte spara filen!");
        }
    }
    else
    {
        Console.WriteLine("Ogiltigt val!");
    }
}