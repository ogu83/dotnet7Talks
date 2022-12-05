using dotnet7Talks.Attributes;

namespace dotnet7Talks.Models;

public class Person
{
    public required string Name { get; set; }

    public required string Surname { get; set; }

    public string? MiddleName { get; set; }

    private string biography
    {
        get
        {
            return $$"""
                        This is a biography of {{{Name}} {{Surname}}}.
                        It has several lines.
                            Some are indented
                                    more than others.
                        Some should start at the first column.
                        Some have "quoted text" in them.
                        """;
        }
    }

    public string Biography => biography;

    [TypeAttribute(typeof(string))]
    [GenericAttribute<string>()] // C# 11 feature
    public override string ToString()
    {
        return $"{Name}{(string.IsNullOrEmpty(MiddleName) ? "" : " ")}{MiddleName} {Surname}";
    }
}