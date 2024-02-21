using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocialMedia.Models;

namespace SocialMedia.Services
{
    public class DatabaseService
    {
        private readonly SocialMediaContext _context;

        public DatabaseService(SocialMediaContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetTableNamesAsync()
        {
            var tableNames = new List<string>();
            var conn = _context.Database.GetDbConnection();
            try
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public' AND table_type = 'BASE TABLE'";
                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        tableNames.Add(reader.GetString(0));
                    }
                }
            }
            finally
            {
                await conn.CloseAsync();
            }
            return tableNames;
        }

    }
}
