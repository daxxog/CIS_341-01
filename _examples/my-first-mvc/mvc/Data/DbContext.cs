using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mvc.Models;

    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext (DbContextOptions<DbContext> options)
            : base(options)
        {
            // https://stackoverflow.com/a/50042017
            Database.EnsureCreated();
        }

        public DbSet<mvc.Models.TodoItem> TodoItem { get; set; } = default!;
    }
