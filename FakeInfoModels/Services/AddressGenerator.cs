using System.Text.RegularExpressions;
using FakeInfoModels;

namespace FakeInfo.Core.Services;

public class AddressGenerator
{
    private readonly Random _random = new();

    private static readonly List<string> StreetNames = new()
    {
        "Nørrebrogade",
        "Vesterbrogade",
        "Østerbrogade",
        "Amagerbrogade",
        "Strandvejen",
        "Hovedgaden",
        "Parkvej",
        "Skovvej",
        "Engvej",
        "Bakkevej",
        "Kirkegade",
        "Stationsvej",
        "Industrivej",
        "Lindevej",
        "Birkevej",
        "Egevej",
        "Søndergade",
        "Vestergade",
        "Østergade",
        "Torvegade"
    };

    private static readonly List<(string PostalCode, string Town)> PostalCodes = LoadPostalCodes();

    public Address GenerateAddress()
    {
        if (PostalCodes.Count == 0)
        {
            throw new InvalidOperationException("No postal codes were loaded from addresses.sql");
        }

        var postal = PostalCodes[_random.Next(PostalCodes.Count)];

        return new Address
        {
            Street = GenerateStreet(),
            Number = GenerateHouseNumber(),
            Floor = GenerateFloor(),
            Door = GenerateDoor(),
            PostalCode = postal.PostalCode,
            Town = postal.Town
        };
    }

    // ✅ FIXET – nu realistiske vejnavne
    private string GenerateStreet()
    {
        return StreetNames[_random.Next(StreetNames.Count)];
    }

    private string GenerateHouseNumber()
    {
        int number = _random.Next(1, 1000);

        if (_random.Next(2) == 0)
            return number.ToString();

        char letter = (char)_random.Next('A', 'Z' + 1);
        return $"{number}{letter}";
    }

    private string GenerateFloor()
    {
        return _random.Next(4) == 0
            ? "st"
            : _random.Next(1, 100).ToString();
    }

    private string GenerateDoor()
    {
        int type = _random.Next(3);

        if (type == 0)
        {
            string[] doors = { "th", "mf", "tv" };
            return doors[_random.Next(doors.Length)];
        }

        if (type == 1)
        {
            return _random.Next(1, 51).ToString();
        }

        char letter = (char)_random.Next('a', 'z' + 1);
        bool useDash = _random.Next(2) == 0;
        int digits = _random.Next(1, 1000);

        return useDash ? $"{letter}-{digits}" : $"{letter}{digits}";
    }

    private static List<(string PostalCode, string Town)> LoadPostalCodes()
    {
        var result = new List<(string PostalCode, string Town)>();

        string possiblePath1 = Path.Combine(AppContext.BaseDirectory, "Data", "addresses.sql");
        string possiblePath2 = Path.Combine(Directory.GetCurrentDirectory(), "Data", "addresses.sql");

        string? sqlPath = null;

        if (File.Exists(possiblePath1))
            sqlPath = possiblePath1;
        else if (File.Exists(possiblePath2))
            sqlPath = possiblePath2;

        if (sqlPath is null)
            return result;

        string sql = File.ReadAllText(sqlPath);

        var matches = Regex.Matches(
            sql,
            @"\('(?<postal>\d{4})',\s*'(?<town>[^']+)'\)"
        );

        foreach (Match match in matches)
        {
            string postal = match.Groups["postal"].Value;
            string town = match.Groups["town"].Value;

            result.Add((postal, town));
        }

        return result;
    }
}