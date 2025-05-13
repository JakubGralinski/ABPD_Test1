using ABPD_Test1.Models;
using Microsoft.Data.SqlClient;

namespace ABPD_Test1.Services;

public class ProjectService : IProjectService
{
    private readonly string _connectionString;

    public ProjectService(IConfiguration config)
        => _connectionString = config.GetConnectionString("DefaultConnection");

    /*public async Task<IEnumerable<ProjectDTO>> GetAllProjects(int id)
    {
        const string sql = "SELECT * FROM Projects";
        
        const string sql2 = "SELECT * FROM Projects WHERE Id = @id";

        await using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        var regs = new List<ProjectDTO>();
        await using (var cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@id", id);
            await using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                regs.Add(new ProjectDTO(){
                    IdProject = rd.GetInt32(0),
                    ProjectName = rd.GetString(1),
                    Deadline = rd.GetDateTime(2)
                });
            }
        }
        return regs;
    }*/

    public async Task<int> DeleteProject(int IdProject, int IdTask)
    {
        const string deleteSql = "DELETE FROM Projects WHERE IdProject = @IdProject AND IdTask = @IdTask";
        
        await using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        await using (var chk = new SqlCommand(
                         "SELECT COUNT(*) FROM Task WHERE IdProject = @IdProject AND IdTask = @IdTask", conn))
        {
            chk.Parameters.AddWithValue("@IdProject", IdProject);
            chk.Parameters.AddWithValue("@IdTask", IdTask);
            var count = await chk.ExecuteScalarAsync();
            if ((int)await chk.ExecuteScalarAsync() == 0)
            {
                throw new KeyNotFoundException("Registr Not Found");
            }
        }
        
        await using (var cmd = new SqlCommand(deleteSql, conn))
        {
            cmd.Parameters.AddWithValue("@IdProject", IdProject);
            cmd.Parameters.AddWithValue("@IdTask", IdTask);
            return await cmd.ExecuteNonQueryAsync();
        }
    }

}