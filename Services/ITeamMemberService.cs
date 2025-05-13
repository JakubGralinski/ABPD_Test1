using ABPD_Test1.Models;

namespace ABPD_Test1.Services;

public interface ITeamMemberService
{
    Task<IEnumerable<TeamMemberDTO>> GetAllTeamMembers(int id);
}