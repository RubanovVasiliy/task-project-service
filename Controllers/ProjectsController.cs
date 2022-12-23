using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using task_project_service.Data;
using task_project_service.Dto;
using task_project_service.Models;

namespace task_project_service.Controllers;

[ApiController]
[Route("projects")]
public class ProjectsController : ControllerBase
{
    private readonly MyDbContext _context;

    public ProjectsController(MyDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route()]
    public async Task<ActionResult<List<Project>>> GetAll()
    {
        var projects = await _context.Projects.ToListAsync();
        return Ok(projects);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Project>> GetById(Guid id)
    {
        var projects = await _context.Projects.Where(p => p.Id == id)
            .Include(t => t.Tasks)
            .FirstOrDefaultAsync();
        if (projects == null)
        {
            return NotFound();
        }

        return Ok(projects);
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<Project>> Add(CreateProjectDto dto)
    {
        if (dto.Status > (ProjectStatus)2)
        {
            return BadRequest("Invalid status specified");
        }

        var project = new Project()
        {
            Name = dto.Name,
            StartDate = dto.StartDate,
            CompleteData = dto.CompleteData,
            Status = dto.Status,
            Priority = dto.Priority
        };
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();

        return Ok(project);
    }

    [HttpPut]
    [Route("update")]
    public async Task<ActionResult<Project>> Update(Project updatedProject)
    {
        if (updatedProject.Status > (ProjectStatus)2)
        {
            return BadRequest("Invalid status specified");
        }

        var project = await _context.Projects.FindAsync(updatedProject.Id);
        if (project == null)
        {
            return NotFound();
        }

        project.Name = updatedProject.Name;
        project.StartDate = updatedProject.StartDate;
        project.CompleteData = updatedProject.CompleteData;
        project.Status = updatedProject.Status;
        project.Priority = updatedProject.Priority;

        await _context.SaveChangesAsync();

        return Ok(updatedProject);
    }

    [HttpDelete]
    [Route("delete/{id}")]
    public async Task<ActionResult<Project>> Delete(Guid id)
    {

        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        _context.Projects.Remove(project);

        await _context.SaveChangesAsync();

        return Ok(project);
    }


    [HttpGet]
    [Route("getByName/{word}")]
    public async Task<ActionResult<List<Project>>> GetByTitle(string word)
    {

        var project = await _context.Projects
            .Where(p => p.Name.ToLower().Contains(word.ToLower()))
            .ToListAsync();

        return Ok(project);
    }
}