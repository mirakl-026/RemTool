using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using RemTool.Models;

namespace RemTool.Services.SqlSE
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Tool> Tools { get; set; }

        public DbSet<SparePart> SpareParts { get; set; }

    }
}
