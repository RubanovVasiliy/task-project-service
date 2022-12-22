namespace task_project_service.Models;

public class CreateProjectDto
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime CompleteData { get; set; }
    public ProjectStatus Status { get; set; }
    public int Priority { get; set; }
}