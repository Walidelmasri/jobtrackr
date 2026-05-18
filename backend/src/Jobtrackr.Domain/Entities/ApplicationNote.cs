namespace Jobtrackr.Domain.Entities;

public class ApplicationNote
{
    public Guid Id { get; private set; }

    public Guid JobApplicationId { get; private set; }

    public JobApplication JobApplication { get; private set; } = null!;

    public string Content { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; }

    private ApplicationNote()
    {
    }

    public ApplicationNote(
        Guid jobApplicationId,
        string content)
    {
        Id = Guid.NewGuid();

        JobApplicationId = jobApplicationId;

        UpdateContent(content);

        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentException("Note content is required.");
        }

        Content = content.Trim();
    }
}