using CSharpFunctionalExtensions;

namespace FriendlyBroccoli.DataAccess.Models;

public class OpenLoop
{
    public Guid Id { get; private set; }
    public Note Note { get; private set; }
    public DateTimeOffset DateCreate { get; private set; }

    public OpenLoop(string noteStr)
    {        
        var note = Note.Create(noteStr);
        if (note.IsFailure)
        {
            throw new ArgumentException(note.Error);
        }

        Note = note.Value;
        GeneralCreate();      
    }

    public OpenLoop(Note note)
    {
        Note = note;
        GeneralCreate();
    }

    private void GeneralCreate()
    {
        Id = Guid.NewGuid();
        DateCreate = DateTimeOffset.UtcNow;
    }

    public static Result<OpenLoop> Create(string noteStr)
    {
        var note = Note.Create(noteStr);
        if (note.IsFailure)
        {
            return Result.Failure<OpenLoop>(note.Error);
        }

        return new OpenLoop(note.Value);
    }
}
