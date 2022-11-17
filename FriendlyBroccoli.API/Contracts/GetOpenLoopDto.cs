namespace FriendlyBroccoli.API.Contracts;

#nullable disable warnings

public sealed class GetOpenLoopDto
{
    public Guid Id { get; set; }
    public string Note { get; set; }
    public DateTimeOffset CreateDate { get; set; }
}