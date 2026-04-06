using NUnit.Framework;
using FakeInfoModels;
using FakeInfo.Core.Services;
using System.Linq;
using System.Collections.Generic;

namespace FakeInfo.Tests;

public class Tests
{
    [Test]
    public void GetCprOnly_ShouldReturnValidCpr()
    {
        // Arrange
        var service = new FakeService();

        // Act
        var result = service.GetCprOnly();

        // Assert
        Assert.That(result.Cpr.Length, Is.EqualTo(10));
        Assert.That(result.Cpr.All(char.IsDigit), Is.True);
    }

    [Test]
    public void GetName_ShouldReturnValidNameAndGender()
    {
        // Arrange
        var service = new FakeService();

        // Act
        var result = service.GetName();

        // Assert
        Assert.That(result.FirstName, Is.Not.Empty);
        Assert.That(result.LastName, Is.Not.Empty);
        Assert.That(result.Gender, Is.EqualTo("male").Or.EqualTo("female"));
    }

    [Test]
    public void GetNameWithDate_ShouldReturnValidDate()
    {
        // Arrange
        var service = new FakeService();

        // Act
        var result = service.GetNameWithDate();

        // Assert
        Assert.That(result.DateOfBirth, Is.LessThan(DateTime.Now));
    }

    [Test]
    public void GetCprName_ShouldReturnValidData()
    {
        // Arrange
        var service = new FakeService();

        // Act
        var result = service.GetCprName();

        // Assert
        Assert.That(result.Cpr.Length, Is.EqualTo(10));
        Assert.That(result.FirstName, Is.Not.Empty);
        Assert.That(result.LastName, Is.Not.Empty);
        Assert.That(result.Gender, Is.EqualTo("male").Or.EqualTo("female"));
    }

    [Test]
    public void GetCprNameAndDate_ShouldMatchCprAndDate()
    {
        // Arrange
        var service = new FakeService();

        // Act
        var result = service.GetCprNameAndDate();

        // Assert
        string expected = result.DateOfBirth.ToString("ddMMyy");
        Assert.That(result.Cpr.StartsWith(expected), Is.True);
    }

    // 
    [Test]
    public void GetCprNameAndDate_ShouldRespectGenderRule()
    {
        // Arrange
        var service = new FakeService();

        // Act + Assert 
        for (int i = 0; i < 50; i++)
        {
            var result = service.GetCprNameAndDate();

            int lastDigit = int.Parse(result.Cpr.Last().ToString());

            Assert.That(
                (result.Gender == "female" && lastDigit % 2 == 0) ||
                (result.Gender == "male" && lastDigit % 2 == 1),
                Is.True
            );
        }
    }

    [Test]
    public void GetPhone_ShouldBeValid()
    {
        // Arrange
        var service = new FakeService();

        // Act
        var result = service.GetPhone();

        // Assert
        Assert.That(result.Phone.Length, Is.EqualTo(8));
        Assert.That(result.Phone.All(char.IsDigit), Is.True);
    }

    [Test]
    public void GetAddress_ShouldBeValid()
    {
        // Arrange
        var service = new FakeService();

        // Act
        var result = service.GetAddress();

        // Assert
        Assert.That(result.Street, Is.Not.Empty);
        Assert.That(result.Number, Is.Not.Empty);
        Assert.That(result.PostalCode, Is.Not.Empty);
        Assert.That(result.Town, Is.Not.Empty);
    }

    [Test]
    public void GetFullPerson_ShouldContainAllInformation()
    {
        // Arrange
        var service = new FakeService();

        // Act
        var result = service.GetFullPerson();

        // Assert
        Assert.That(result.Cpr.Length, Is.EqualTo(10));
        Assert.That(result.FirstName, Is.Not.Empty);
        Assert.That(result.LastName, Is.Not.Empty);
        Assert.That(result.Phone.Length, Is.EqualTo(8));
        Assert.That(result.Address, Is.Not.Null);
    }

    [Test]
    public void GetPersons_ShouldReturnBetween2And100()
    {
        // Arrange
        var service = new FakeService();

        // Act
        var result = service.GetPersons(10);

        // Assert
        Assert.That(result.Count, Is.GreaterThanOrEqualTo(2));
        Assert.That(result.Count, Is.LessThanOrEqualTo(100));
    }

    // WHITE-BOX: under minimum
    [Test]
    public void GetPersons_WithTooLowInput_ShouldReturnMinimum2()
    {
        // Arrange
        var service = new FakeService();

        // Act
        var result = service.GetPersons(1);

        // Assert
        Assert.That(result.Count, Is.GreaterThanOrEqualTo(2));
    }

    // WHITE-BOX: over maximum
    [Test]
    public void GetPersons_WithTooHighInput_ShouldReturnMaximum100()
    {
        // Arrange
        var service = new FakeService();

        // Act
        var result = service.GetPersons(200);

        // Assert
        Assert.That(result.Count, Is.LessThanOrEqualTo(100));
    }

    // WHITE-BOX: boundary = 2
    [Test]
    public void GetPersons_WithExactly2_ShouldReturn2()
    {
        // Arrange
        var service = new FakeService();

        // Act
        var result = service.GetPersons(2);

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));
    }

    // WHITE-BOX: boundary = 100
    [Test]
    public void GetPersons_WithExactly100_ShouldReturn100()
    {
        // Arrange
        var service = new FakeService();

        // Act
        var result = service.GetPersons(100);

        // Assert
        Assert.That(result.Count, Is.EqualTo(100));
    }
}