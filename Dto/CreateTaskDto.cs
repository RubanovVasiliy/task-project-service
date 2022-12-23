using TaskStatus = task_project_service.Models.TaskStatus;

namespace task_project_service.Dto;

public class CreateTaskDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskStatus Status { get; set; }
    public int Priority { get; set; }
}