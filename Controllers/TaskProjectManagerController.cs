using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using task_project_service.Data;
using task_project_service.Dto;
using task_project_service.Models;

namespace task_project_service.Controllers;

[ApiController]
[Route("taskProject")]
public class TaskProjectManagerController : ControllerBase
{
    private readonly MyDbContext _context;

    public TaskProjectManagerController(MyDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Route("set")]
    public async Task<ActionResult<Project>> SetTaskToProject(ProjectTaskDto dto)
    {
        var project = await _context.Projects
            .Where(p => p.Id == dto.ProjectId)
            .Include(t => t.Tasks)
            .FirstOrDefaultAsync();

        if (project == null) return NotFound($"Project ID: {dto.ProjectId} not found");

        var task = await _context.Tasks.FindAsync(dto.TaskId);
        if (task == null) return NotFound($"Task ID: {dto.TaskId} not found");
        project.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return Ok(project);
    }

    [HttpPost]
    [Route("delete")]
    public async Task<ActionResult<Project>> RemoveTaskToProject(ProjectTaskDto dto)
    {
        var project = await _context.Projects
            .Where(p => p.Id == dto.ProjectId)
            .Include(t => t.Tasks)
            .FirstOrDefaultAsync();

        if (project == null)
        {
            return NotFound($"Project ID: {dto.ProjectId} not found");
        }

        foreach (var task in project.Tasks.Where(t => t.Id == dto.TaskId))
        {
            project.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return Ok(project);
        }

        await _context.SaveChangesAsync();
        return NotFound($"Task ID: {dto.TaskId} not found");
    }
}