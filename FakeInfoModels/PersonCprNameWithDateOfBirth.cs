namespace FakeInfoModels;

public class PersonCprNameWithDateOfBirth
{
    public string Cpr { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Gender { get; set; } = "";
    public DateTime DateOfBirth { get; set; }
}