using CSharpFunctionalExtensions;
using FriendlyBroccoli.DataAccess.Models;

namespace FriendlyBroccoli.DataAccess;

public class Repository
{
    private readonly static List<OpenLoop> _openLoops = new();

    static Repository()
    {
        _openLoops.Add(new OpenLoop("note 1"));        
        _openLoops.Add(new OpenLoop("note 2"));
    }

    public static IEnumerable<OpenLoop> Get() => _openLoops;

    public static Result<Guid> CreateNew(string noteStr)
    {
        var note = Note.Create(noteStr);
        if (note.IsFailure)
        {
            return Result.Failure<Guid>(note.Error);
        }

        var openLoop = new OpenLoop(note.Value);
        _openLoops.Add(openLoop);

        return openLoop.Id;
    }
}
