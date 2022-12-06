using System.Runtime.Serialization;
using dotnet7Talks.Attributes;

namespace dotnet7Talks.Models;

[DataContract(Name = nameof(Person))] // C# 11 feature Extended nameof scope
public class Person
{
    public nint MyIntPtr; // C# 11 feature

    public nuint MyUnsignedIntPtr; // C# 11 feature

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

    public string FullName { get { return $"{Name}{(string.IsNullOrEmpty(MiddleName) ? "" : " ")}{MiddleName} {Surname}"; } }


    [TypeAttribute(typeof(string))]
    [GenericAttribute<string>()] // C# 11 feature
    public override string ToString() => FullName;
}