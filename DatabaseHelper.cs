using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SwagLabs.UI.Tests
{
    public class DatabaseHelper
    {
        // En un proyecto real, esto se lee de un archivo de configuración seguro (.env o appsettings)
        private readonly string _connectionString = "Server=localhost;Database=QA_TestDB;User Id=sa;Password=tu_password_fuerte;TrustServerCertificate=True;";

        public async Task<int> GetUserCountAsync(string username)
        {
            // El 'using' asegura que la conexión se cierre y libere memoria automáticamente
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = "SELECT COUNT(*) FROM Users WHERE Username = @username";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);

            var result = await command.ExecuteScalarAsync();
            return (int)result;
        }
    }
}