namespace FakeInfoModels;

public class PersonFull
{
    public string Cpr { get; set; } = "";
    public DateTime DateOfBirth { get; set; }
    public string Phone { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Gender { get; set; } = "";
    public Address Address { get; set; } = new();
}