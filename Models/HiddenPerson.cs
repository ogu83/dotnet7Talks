file interface IPerson
{
    string ProvideName();
}

file class Person
{
    public string Work() => "Ethan Hunt";
}

public class HiddenPerson : IPerson
{
    public string ProvideName()
    {
        var worker = new Person();
        return worker.Work();
    }
}
