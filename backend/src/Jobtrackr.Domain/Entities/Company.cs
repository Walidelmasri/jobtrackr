namespace Jobtrackr.Domain.Entities;

public class Company
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string? Website { get; private set; }

    public bool SponsorsVisa { get; private set; }

    public List<JobApplication> Applications { get; private set; } = [];

    private Company()
    {
    }

    public Company(
        string name,
        string? website,
        bool sponsorsVisa)
    {
        Id = Guid.NewGuid();

        SetName(name);

        Website = website;
        SponsorsVisa = sponsorsVisa;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Company name is required.");
        }

        Name = name.Trim();
    }

    public void UpdateWebsite(string? website)
    {
        Website = website?.Trim();
    }

    public void UpdateVisaSponsorship(bool sponsorsVisa)
    {
        SponsorsVisa = sponsorsVisa;
    }
}