using System.Text.Json;
using FakeInfoModels;

namespace FakeInfo.Core.Services;

public class NameGenerator
{
    private readonly Random _random = new();

    private static readonly List<PersonNameSource> Persons = LoadPersons();

    public PersonName GenerateName()
    {
        if (Persons.Count == 0)
        {
            throw new InvalidOperationException("No persons were loaded from person-names.json");
        }

        var person = Persons[_random.Next(Persons.Count)];

        return new PersonName
        {
            FirstName = CleanFirstName(person.Name),
            LastName = person.Surname,
            Gender = person.Gender
        };
    }

    private static List<PersonNameSource> LoadPersons()
    {
        string[] possiblePaths =
        [
            Path.Combine(AppContext.BaseDirectory, "Data", "person-names.json"),
            Path.Combine(Directory.GetCurrentDirectory(), "Data", "person-names.json")
        ];

        string? jsonPath = possiblePaths.FirstOrDefault(File.Exists);

        if (string.IsNullOrWhiteSpace(jsonPath))
        {
            return [];
        }

        string json = File.ReadAllText(jsonPath);

        var wrapper = JsonSerializer.Deserialize<PersonNameWrapper>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        return wrapper?.Persons ?? [];
    }

    private string CleanFirstName(string rawName)
    {
        if (string.IsNullOrWhiteSpace(rawName))
        {
            return "Ukendt";
        }

        return rawName.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0];
    }
}