namespace FakeInfo.Core.Services;

public class CprGenerator
{
    private readonly Random _random = new();

    public string GenerateCpr(string gender, DateTime dateOfBirth)
    {
        var datePart = dateOfBirth.ToString("ddMMyy");
        var firstThree = _random.Next(100, 1000).ToString();

        int lastDigit;
        if (gender.ToLower() == "female")
        {
            lastDigit = GetRandomEvenDigit();
        }
        else
        {
            lastDigit = GetRandomOddDigit();
        }

        return datePart + firstThree + lastDigit;
    }

    private int GetRandomEvenDigit()
    {
        int[] evenDigits = [0, 2, 4, 6, 8];
        return evenDigits[_random.Next(evenDigits.Length)];
    }

    private int GetRandomOddDigit()
    {
        int[] oddDigits = [1, 3, 5, 7, 9];
        return oddDigits[_random.Next(oddDigits.Length)];
    }
}