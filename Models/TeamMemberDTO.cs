using System.ComponentModel.DataAnnotations;

namespace ABPD_Test1.Models;

public class TeamMemberDTO
{
    public int IdTeamMember { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Required, EmailAddress] public string Email { get; set; }
}