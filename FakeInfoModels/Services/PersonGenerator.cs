using FakeInfoModels;

namespace FakeInfo.Core.Services;

public class PersonGenerator
{
    private readonly NameGenerator _nameGenerator = new();
    private readonly AddressGenerator _addressGenerator = new();
    private readonly PhoneGenerator _phoneGenerator = new();
    private readonly CprGenerator _cprGenerator = new();
    private readonly Random _random = new();

    public PersonName GenerateNameOnly()
    {
        return _nameGenerator.GenerateName();
    }

    public PersonNameWithDateOfBirth GenerateNameAndDateOfBirth()
    {
        var name = _nameGenerator.GenerateName();
        var dateOfBirth = GenerateDateOfBirth();

        return new PersonNameWithDateOfBirth
        {
            FirstName = name.FirstName,
            LastName = name.LastName,
            Gender = name.Gender,
            DateOfBirth = dateOfBirth
        };
    }

    public PersonCprOnly GenerateCprOnly()
    {
        var name = _nameGenerator.GenerateName();
        var dateOfBirth = GenerateDateOfBirth();
        var cpr = _cprGenerator.GenerateCpr(name.Gender, dateOfBirth);

        return new PersonCprOnly
        {
            Cpr = cpr
        };
    }

    public PersonCprName GenerateCprAndName()
    {
        var name = _nameGenerator.GenerateName();
        var dateOfBirth = GenerateDateOfBirth();
        var cpr = _cprGenerator.GenerateCpr(name.Gender, dateOfBirth);

        return new PersonCprName
        {
            Cpr = cpr,
            FirstName = name.FirstName,
            LastName = name.LastName,
            Gender = name.Gender
        };
    }

    public PersonCprNameWithDateOfBirth GenerateCprNameAndDateOfBirth()
    {
        var name = _nameGenerator.GenerateName();
        var dateOfBirth = GenerateDateOfBirth();
        var cpr = _cprGenerator.GenerateCpr(name.Gender, dateOfBirth);

        return new PersonCprNameWithDateOfBirth
        {
            Cpr = cpr,
            FirstName = name.FirstName,
            LastName = name.LastName,
            Gender = name.Gender,
            DateOfBirth = dateOfBirth
        };
    }

    public Address GenerateAddressOnly()
    {
        return _addressGenerator.GenerateAddress();
    }

    public PhoneOnly GeneratePhoneOnly()
    {
        return new PhoneOnly
        {
            Phone = _phoneGenerator.GeneratePhoneNumber()
        };
    }

    public PersonFull GenerateFullPerson()
    {
        var name = _nameGenerator.GenerateName();
        var dateOfBirth = GenerateDateOfBirth();
        var cpr = _cprGenerator.GenerateCpr(name.Gender, dateOfBirth);
        var address = _addressGenerator.GenerateAddress();
        var phone = _phoneGenerator.GeneratePhoneNumber();

        return new PersonFull
        {
            Cpr = cpr,
            DateOfBirth = dateOfBirth,
            Phone = phone,
            FirstName = name.FirstName,
            LastName = name.LastName,
            Gender = name.Gender,
            Address = address
        };
    }

    public List<PersonFull> GenerateBulk(int count)
    {
        var list = new List<PersonFull>();

        for (int i = 0; i < count; i++)
        {
            list.Add(GenerateFullPerson());
        }

        return list;
    }

    private DateTime GenerateDateOfBirth()
    {
        var start = new DateTime(1950, 1, 1);
        var end = new DateTime(2010, 12, 31);

        var range = (end - start).Days;
        return start.AddDays(_random.Next(range + 1));
    }
}