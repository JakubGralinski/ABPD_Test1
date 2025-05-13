using ABPD_Test1.Models;
using Microsoft.Data.SqlClient;

namespace ABPD_Test1.Services;

public class TaskService : ITaskService
{
    private readonly string _connectionString;

    public TaskService(IConfiguration config)
        => _connectionString = config.GetConnectionString("DefaultConnection");
    
    public async Task<IEnumerable<TaskDTO>> GetAllTasks(int id)
    {
        const string sql = "SELECT * FROM Task";
        
        const string sql2 = "SELECT * FROM Task WHERE Id = @id";

        await using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        var regs = new List<TaskDTO>();
        await using (var cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@id", id);
            await using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                regs.Add(new TaskDTO(){
                    IdTask = rd.GetInt32(0),
                    IdTaskType = rd.GetInt32(1),
                    Name = rd.GetString(2),
                    Description = rd.GetString(3),
                    Deadline = rd.GetDateTime(4),
                    IdProject = rd.GetInt32(5),
                    IdAssignedTo = rd.GetInt32(6),
                    IdCreator = rd.GetInt32(7)
                    
                });
            }
        }
        return regs;
    }

    public async Task<int> DeleteTask(int IdTask, int IdProject)
    {
        const string deleteSql = "DELETE FROM Task WHERE IdTask = @IdTask AND IdProject = @IdProject";
        
        await using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        await using (var chk = new SqlCommand(
                         "SELECT COUNT(*) FROM Task WHERE IdTask = @IdTask AND IdProject = @IdProject", conn))
        {
            chk.Parameters.AddWithValue("@IdTask", IdTask);
            chk.Parameters.AddWithValue("@IdProject", IdProject);
            var count = await chk.ExecuteScalarAsync();
            if ((int)await chk.ExecuteScalarAsync() == 0)
            {
                throw new KeyNotFoundException("Registr Not Found");
            }
        }
        
        await using (var cmd = new SqlCommand(deleteSql, conn))
        {
            cmd.Parameters.AddWithValue("@IdTask", IdTask);
            cmd.Parameters.AddWithValue("@IdProject", IdProject);
            return await cmd.ExecuteNonQueryAsync();
        }
    }
}