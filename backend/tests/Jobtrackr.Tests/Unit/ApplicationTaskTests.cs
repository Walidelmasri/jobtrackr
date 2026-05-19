using Jobtrackr.Domain.Entities;

namespace Jobtrackr.Tests.Unit.Entities;

public class ApplicationTaskTests
{
    [Fact]
    public void Constructor_ShouldSetValues()
    {
        var task = new ApplicationTask(
            Guid.NewGuid(),
            "Follow up",
            DateTime.UtcNow.AddDays(2));

        Assert.Equal(
            "Follow up",
            task.Title);

        Assert.False(
            task.IsCompleted);
    }

    [Fact]
    public void Complete_ShouldSetCompleted()
    {
        var task = new ApplicationTask(
            Guid.NewGuid(),
            "Follow up",
            DateTime.UtcNow);

        task.Complete();

        Assert.True(
            task.IsCompleted);
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenTitleEmpty()
    {
        Assert.Throws<ArgumentException>(() =>
            new ApplicationTask(
                Guid.NewGuid(),
                "",
                DateTime.UtcNow));
    }
}