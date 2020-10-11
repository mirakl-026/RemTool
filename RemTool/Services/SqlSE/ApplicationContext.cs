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
            //Database.EnsureCreated();
        }

        #region Brands implementation
        public DbSet<Brand> Brands { get; set; }

        public IEnumerable<Brand> GetAllBrands()
        {
            return this.Brands.ToList();
        }

        public void CreateBrand(Brand brand)
        {
            this.Brands.Add(brand);
            this.SaveChanges();
        }

        public Brand ReadBrand(int id)
        {
            Brand brand = this.Brands.FirstOrDefault(x => x.Id == id);
            return brand;
        }

        public void UpdateBrand(Brand brand)
        {
            this.Brands.Update(brand);
            this.SaveChanges();
        }

        public void DeleteBrand(int id)
        {
            Brand brand = this.Brands.FirstOrDefault(x => x.Id == id);
            if (brand != null)
            {
                this.Brands.Remove(brand);
                this.SaveChanges();
            }
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
            return this.Tools.ToList();
        }

        public void CreateTool(Tool tool)
        {
            this.Tools.Add(tool);
            this.SaveChanges();
        }

        public Tool ReadTool(int id)
        {
            Tool tool = this.Tools.FirstOrDefault(x => x.Id == id);
            return tool;
        }

        public void UpdateTool(Tool tool)
        {
            this.Tools.Update(tool);
            this.SaveChanges();
        }

        public void DeleteTool(int id)
        {
            Tool tool = this.Tools.FirstOrDefault(x => x.Id == id);
            if (tool != null)
            {
                this.Tools.Remove(tool);
                this.SaveChanges();
            }
        }

        public void DeleteAllTools()
        {
            throw new NotImplementedException();
        }
        #endregion




        #region SpareParts implementation
        public DbSet<SparePart> SpareParts { get; set; }

        public IEnumerable<SparePart> GetAllSpareParts()
        {
            return this.SpareParts.ToList();
        }

        public void CreateSparePart(SparePart sparePart)
        {
            this.SpareParts.Add(sparePart);
            this.SaveChanges();
        }

        public SparePart ReadSparePart(int id)
        {
            SparePart sparePart = this.SpareParts.FirstOrDefault(x => x.Id == id);
            return sparePart;
        }

        public void UpdateSparePart(SparePart sparePart)
        {
            this.SpareParts.Update(sparePart);
            this.SaveChanges();
        }

        public void DeleteSparePart(int id)
        {
            SparePart sparePart = this.SpareParts.FirstOrDefault(x => x.Id == id);
            if (sparePart != null)
            {
                this.SpareParts.Remove(sparePart);
                this.SaveChanges();
            }
        }

        public void DeleteAllSpareParts()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
