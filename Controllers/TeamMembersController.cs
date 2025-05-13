using ABPD_Test1.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABPD_Test1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamMemberController : ControllerBase
{
    private readonly ITeamMemberService _teamMemberService;

    public TeamMemberController(ITeamMemberService teamMemberService)
        => _teamMemberService = teamMemberService;

    [HttpGet("{id}/members")]
    public async Task<IActionResult> GetAllTeamMembers(int id)
    {
        try
        {
            var members = await _teamMemberService.GetAllTeamMembers(id);
            return Ok(members);
        }
        catch (KeyNotFoundException knf)
        {
            return NotFound(knf.Message);
        }
    }
}