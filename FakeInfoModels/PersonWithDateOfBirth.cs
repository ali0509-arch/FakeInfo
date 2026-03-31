namespace FakeInfo.Core.Models;

public class PersonWithDateOfBirth
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Gender { get; set; } = "";
    public DateTime DateOfBirth { get; set; }
}