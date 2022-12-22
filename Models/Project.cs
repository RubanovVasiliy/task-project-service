using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace task_project_service.Models;

public class Project
{
    [Key] public Guid Id { get; set; }
    [Required] public string Name { get; set; }
    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime CompleteData { get; set; }
    [Required] public ProjectStatus Status { get; set; }
    [Required] public int Priority { get; set; }
    [JsonIgnore] public List<Task>? Tasks { get; set; }
}