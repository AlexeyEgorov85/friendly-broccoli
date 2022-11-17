using FriendlyBroccoli.DataAccess.Models;

namespace FriendlyBroccoli.UnitTests;

public class OpenLoopsTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    public void Ctor_InvalidArgument(string? value)
    {
        Assert.Throws<ArgumentException>(() => new OpenLoop(value!));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    public void Create_InvalidValid(string? value)
    {
        var result = OpenLoop.Create(value!);

        Assert.True(result.IsFailure);
        Assert.False(string.IsNullOrEmpty(result.Error));
    }

    [Fact]
    public void Ctor_ValidArgument()
    {
        var openLoop = new OpenLoop("note");

        Assert.True(openLoop.Id != Guid.Empty);
        Assert.True(openLoop.DateCreate != DateTimeOffset.MinValue);
    }

    [Fact]
    public void Create_ValidArgument()
    {
        var result = OpenLoop.Create("note");

        Assert.True(result.IsSuccess);
        Assert.True(result.Value.Id != Guid.Empty);
        Assert.True(result.Value.DateCreate != DateTimeOffset.MinValue);
    }
}
