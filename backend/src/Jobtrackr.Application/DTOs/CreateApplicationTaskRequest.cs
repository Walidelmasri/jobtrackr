namespace Jobtrackr.Application.DTOs;

public class CreateApplicationTaskRequest
{
    public string Title { get; set; } = string.Empty;

    public DateTime DueDate { get; set; }
}