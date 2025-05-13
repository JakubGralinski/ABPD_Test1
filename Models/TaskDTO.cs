using System.ComponentModel.DataAnnotations;

namespace ABPD_Test1.Models;

public class TaskDTO
{
    public int IdTask { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public int IdProject { get; set; }
    public int IdTaskType { get; set; }
    public int IdAssignedTo { get; set; }
    public int IdCreator { get; set; }
}