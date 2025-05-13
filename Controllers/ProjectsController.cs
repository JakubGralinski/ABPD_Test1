using ABPD_Test1.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABPD_Test1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
        => _projectService = projectService;
    
    
    [HttpDelete("{id}/projects/{IdProject}")]
    public async Task<IActionResult> DeleteTask(int IdProject, int IdTask)
    {
        try
        {
            await _projectService.DeleteProject(IdProject, IdTask);
            return NoContent();
        }
        catch (KeyNotFoundException knf)
        {
            return NotFound(knf.Message);
        }
    }
}
