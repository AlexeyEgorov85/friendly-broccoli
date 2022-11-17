using CSharpFunctionalExtensions;

namespace FriendlyBroccoli.DataAccess.Models;

public record struct Note
{
    public string Value { get; init; }

    public static Result<Note> Create(string note)
    {
        if (string.IsNullOrWhiteSpace(note))
        {
            return Result.Failure<Note>("note cannot be null or empty");
        }

        return new Note() { Value = note };
    }
}