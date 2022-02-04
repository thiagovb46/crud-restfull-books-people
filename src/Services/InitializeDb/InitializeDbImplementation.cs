using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestWith.NET.Context;

namespace src.Services.InitializeDb
{
    public static class InitializeDbImplementation 
    {
        private static readonly DbContext _context;

    public static void Initialize (DataContext context)
    {
         var pendingMigrations = _context.Database
                .GetPendingMigrations()
                .ToList();
            
            if (pendingMigrations.Any())
            {
                var migrationName = pendingMigrations.FirstOrDefault() ?? "migration";
                var name = $"_{migrationName}";
                _context.Database.Migrate();
            }
    }
    }
}