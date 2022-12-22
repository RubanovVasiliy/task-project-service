namespace task_project_service.Models;

public class CreateProjectDto
{
    public string Name { get; set; }
    public string StartDate { get; set; }
    public string CompleteData { get; set; }
    public ProjectStatus Status { get; set; }
    public int Priority { get; set; }
}