using Jobtrackr.Domain.Entities;

namespace Jobtrackr.Tests.Unit;

public class ApplicationNoteTests
{
    [Fact]
    public void Constructor_ShouldThrow_WhenContentIsEmpty()
    {
        Assert.Throws<ArgumentException>(() =>
            new ApplicationNote(
                Guid.NewGuid(),
                ""));
    }

    [Fact]
    public void Constructor_ShouldTrimContent()
    {
        var note = new ApplicationNote(
            Guid.NewGuid(),
            "  Follow up next week  ");

        Assert.Equal("Follow up next week", note.Content);
    }
}