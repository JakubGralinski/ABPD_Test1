using System.ComponentModel.DataAnnotations;

namespace ABPD_Test1.Models;

public class ProjectDTO
{
    public int IdProject { get; set; }
    public string ProjectName { get; set; }
    public DateTime Deadline { get; set; }
}