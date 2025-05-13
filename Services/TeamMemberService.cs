using ABPD_Test1.Models;
using Microsoft.Data.SqlClient;

namespace ABPD_Test1.Services;

public class TeamMemberService : ITeamMemberService
{
    private readonly string _connectionString;

    public TeamMemberService(IConfiguration config)
        => _connectionString = config.GetConnectionString("DefaultConnection");
    
    public async Task<IEnumerable<TeamMemberDTO>> GetAllTeamMembers(int id)
    {
        const string sql = "SELECT * FROM TeamMember";
        
        const string sql2 = "SELECT * FROM TeamMember WHERE Id = @id";

        await using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        var regs = new List<TeamMemberDTO>();
        await using (var cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@id", id);
            await using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                regs.Add(new TeamMemberDTO(){
                    IdTeamMember = rd.GetInt32(1),
                    FirstName = rd.GetString(2),
                    LastName = rd.GetString(3),
                    Email = rd.GetString(4)
                });
            }
        }
        return regs;
    }
}