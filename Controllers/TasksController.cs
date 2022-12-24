using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using task_project_service.Data;
using task_project_service.Dto;


namespace task_project_service.Controllers;

[ApiController]
[Route("tasks")]
public class TasksController : ControllerBase
{
    private readonly MyDbContext _context;

    public TasksController(MyDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("getAll")]
    public async Task<ActionResult<List<Models.Task>>> GetAll()
    {
        var task = await _context.Tasks.ToListAsync();
        return Ok(task);
    }

    [HttpGet]
    [Route("getById/{id}")]
    public async Task<ActionResult<Models.Task>> GetById(Guid id)
    {
        var tasks = await _context.Tasks.FindAsync(id);
        if (tasks == null)
        {
            return NotFound();
        }

        return Ok(tasks);
    }

    [HttpPost]
    [Route("add")]
    public async Task<ActionResult<Models.Task>> Add(CreateTaskDto dto)
    {
        if (dto.Status > (Models.TaskStatus)2)
        {
            return BadRequest("Invalid status specified");
        }
        
        var task = new Models.Task()
        {
            Name = dto.Name,
            Description = dto.Description,
            Status = dto.Status,
            Priority = dto.Priority
        };

        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();

        return Ok(task);
    }

    [HttpPut]
    [Route("update")]
    public async Task<ActionResult<Models.Task>> Update(Models.Task updatedTask)
    {
        var task = await _context.Tasks.FindAsync(updatedTask.Id);
        if (task == null)
        {
            return NotFound();
        }

        task.Name = updatedTask.Name;
        task.Description = updatedTask.Description;
        task.Status = updatedTask.Status;
        task.Priority = updatedTask.Priority;

        await _context.SaveChangesAsync();

        return Ok(updatedTask);
    }

    [HttpDelete]
    [Route("delete/{id}")]
    public async Task<ActionResult<Models.Task>> Delete(Guid id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return Ok(task);
    }


    [HttpGet]
    [Route("getByName/{word}")]
    public async Task<ActionResult<List<Models.Task>>> GetByTitle(string word)
    {
        var task = await _context.Tasks
            .Where(p => p.Name.ToLower().Contains(word.ToLower()))
            .ToListAsync();
        return Ok(task);
    }
}