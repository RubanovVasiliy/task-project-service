using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using task_project_service.Data;
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
    [Route("getAll")]
    public async Task<ActionResult<List<Project>>> GetAll()
    {
        var projects = await _context.Projects.ToListAsync();
        return Ok(projects);
    }

    [HttpGet]
    [Route("getById/{id:int}")]
    public async Task<ActionResult<Project>> GetById(int id)
    {
        var projects = await _context.Projects.FindAsync(id);
        if (projects == null)
        {
            return NotFound();
        }

        return Ok(projects);
    }

    [HttpPost]
    [Route("add")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Project>> Add(CreateProjectDto dto)
    {
        try
        {
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
        catch
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpPut]
    [Route("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Project>> Update(Project updatedProject)
    {
        try
        {
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
        catch
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpDelete]
    [Route("delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Project>> Delete(int id)
    {
        try
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
        catch
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }


    [HttpGet]
    [Route("getByName/{word}")]
    public async Task<ActionResult<List<Project>>> GetByTitle(string word)
    {
        try
        {
            var project = await _context.Projects
                .Where(p => p.Name.ToLower().Contains(word.ToLower()))
                .ToListAsync();
            
            return Ok(project);
        }
        catch
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}