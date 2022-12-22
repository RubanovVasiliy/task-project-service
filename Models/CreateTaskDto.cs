namespace task_project_service.Models;

public class CreateTaskDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskStatus Status { get; set; }
    public int Priority { get; set; }
}