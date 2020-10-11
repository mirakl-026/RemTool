using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using RemTool.Models;
using RemTool.Infrastructure.Interfaces.Services;

namespace RemTool.Services.SqlSE
{
    public class ApplicationContext : DbContext, IBrandService, IToolService, ISparePartService
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        #region Brands implementation
        public DbSet<Brand> Brands { get; set; }

        public IEnumerable<Brand> GetAllBrands()
        {
            throw new NotImplementedException();
        }

        public void CreateBrand(Brand brand)
        {
            throw new NotImplementedException();
        }

        public Brand ReadBrand(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateBrand(Brand brand)
        {
            throw new NotImplementedException();
        }

        public void DeleteBrand(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteAllBrands()
        {
            throw new NotImplementedException();
        }
        #endregion




        #region Tools implementation
        public DbSet<Tool> Tools { get; set; }

        public IEnumerable<Tool> GetAllTools()
        {
            throw new NotImplementedException();
        }

        public void CreateTool(Tool tool)
        {
            throw new NotImplementedException();
        }

        public Tool ReadTool(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateTool(Tool tool)
        {
            throw new NotImplementedException();
        }

        public void DeleteTool(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteAllTools()
        {
            throw new NotImplementedException();
        }
        #endregion




        #region SpareParts implementation
        public DbSet<SparePart> SpareParts { get; set; }

        public IEnumerable<Tool> GetAllSpareParts()
        {
            throw new NotImplementedException();
        }

        public void CreateSparePart(SparePart sparePart)
        {
            throw new NotImplementedException();
        }

        public SparePart ReadSparePart(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateSparePart(SparePart sparePart)
        {
            throw new NotImplementedException();
        }

        public void DeleteSparePart(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteAllSpareParts()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
