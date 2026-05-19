namespace Jobtrackr.Domain.Entities;

public class ApplicationTask
{
    public Guid Id { get; private set; }

    public Guid JobApplicationId { get; private set; }

    public JobApplication JobApplication { get; private set; } = null!;

    public string Title { get; private set; } = string.Empty;

    public DateTime DueDate { get; private set; }

    public bool IsCompleted { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private ApplicationTask()
    {
    }

    public ApplicationTask(
        Guid jobApplicationId,
        string title,
        DateTime dueDate)
    {
        Id = Guid.NewGuid();

        JobApplicationId = jobApplicationId;

        SetTitle(title);

        DueDate = dueDate;

        CreatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        IsCompleted = true;
    }

    public void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException(
                "Task title required");
        }

        Title = title.Trim();
    }
}