using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace task_project_service.Models;

public class Task
{
    [Key] public Guid Id { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string Description { get; set; }
    [Required] public TaskStatus Status { get; set; }
    [Required] public int Priority { get; set; }
}