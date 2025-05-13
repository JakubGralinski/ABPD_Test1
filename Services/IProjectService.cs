using ABPD_Test1.Models;

namespace ABPD_Test1.Services;

public interface IProjectService
{
    /*Task<IEnumerable<ProjectDTO>> GetAllProjects(int id);*/
    /*Task<int> CreateProject(ProjectDTO project);
    Task<int> UpdateProject(ProjectDTO project);*/
    Task<int> DeleteProject(int IdProject, int IdTask);
    /*Task<ProjectDTO> GetProjectById(int id);*/
}