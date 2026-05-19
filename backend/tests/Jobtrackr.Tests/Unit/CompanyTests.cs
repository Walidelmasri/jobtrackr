using Jobtrackr.Domain.Entities;

namespace Jobtrackr.Tests.Unit;

public class CompanyTests
{
    [Fact]
    public void Constructor_ShouldThrow_WhenNameIsEmpty()
    {
        Assert.Throws<ArgumentException>(() =>
            new Company(
                "",
                "https://example.com",
                true));
    }

    [Fact]
    public void Constructor_ShouldTrimCompanyName()
    {
        var company = new Company(
            "  Microsoft  ",
            "https://microsoft.com",
            true);

        Assert.Equal("Microsoft", company.Name);
    }
}