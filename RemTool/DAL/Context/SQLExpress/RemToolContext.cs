using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using RemTool.Models;

namespace RemTool.DAL.Context.SQLExpress
{
    public class RemToolContext : DbContext
    {
        public RemToolContext(DbContextOptions<RemToolContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Tool> Tools { get; set; }

        public DbSet<SparePart> SpareParts { get; set; }
    }
}
