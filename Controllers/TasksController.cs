using ABPD_Test1.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABPD_Test1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService clientService)
        => _taskService = clientService;

    [HttpGet("{id}/tasks")]
    public async Task<IActionResult> GetAllTasks(int id)
    {
        try
        {
            var tasks = await _taskService.GetAllTasks(id);
            return Ok(tasks);
        }
        catch (KeyNotFoundException knf)
        {
            return NotFound(knf.Message);
        }
    }
    
    [HttpDelete("{id}/tasks/{IdTask}")]
    public async Task<IActionResult> DeleteTask(int IdTask, int IdProject)
    {
        try
        {
            await _taskService.DeleteTask(IdTask, IdProject);
            return NoContent();
        }
        catch (KeyNotFoundException knf)
        {
            return NotFound(knf.Message);
        }
    }
}