using dotnet7Talks.Models;

public interface IPersonService 
{
    Person GetPerson();
    string GetPersonBiography();
}

public class PersonService : IPersonService
{
    public Person GetPerson()
    {
       return new Person() { Name = "Oguz", Surname = "Koroglu" };
    }

    public string GetPersonBiography()
    {
        return new Person() { Name = "Oguz", Surname = "Koroglu" }.Biography;
    }
}