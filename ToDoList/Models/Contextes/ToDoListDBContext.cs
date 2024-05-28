using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ToDoList.Models.Contextes
{
    public class ToDoListDBContext
    {
        private readonly IConfiguration configuration;
        private readonly string? connectionString;

        public ToDoListDBContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = this.configuration.GetConnectionString("DBConnection");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
