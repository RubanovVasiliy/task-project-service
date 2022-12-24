using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace task_project_service.Models;

public class Project
{
    [Key] public Guid Id { get; set; }
    [Required] public string Name { get; set; }
    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime CompleteData { get; set; }
    [AllowNull] public ProjectStatus Status { get; set; }
    [AllowNull] public int Priority { get; set; }
    [JsonIgnore] public List<Task> Tasks { get; set; } = new();
}