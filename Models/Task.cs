using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace task_project_service.Models;

public class Task
{
    [Key] public Guid Id { get; set; }
    [Required] public string Name { get; set; }
    public string Description { get; set; }
    public TaskStatus Status { get; set; }
    public int Priority { get; set; }
    public Guid? ProjectId { get; set; }
}