using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace task_project_service.Models;

public class Task
{
    [Key] public Guid Id { get; set; }
    [Required] public string Name { get; set; }
    [ AllowNull] public string Description { get; set; }
    [ AllowNull] public TaskStatus Status { get; set; }
    [ AllowNull] public int Priority { get; set; }
    public Guid? ProjectId { get; set; }
}