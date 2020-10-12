using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RemTool.Models;
using RemTool.Infrastructure.Interfaces.Services;
using RemTool.DAL.Context.SQLExpress;

namespace RemTool.Services.SQLExpress
{
    public class SparePartService : ISparePartService
    {
        public RemToolContext _db;

        public SparePartService(RemToolContext db)
        {
            _db = db;
            if (!_db.SpareParts.Any())
            {
                _db.SpareParts.Add(new SparePart { Name = "Cooler", Description = "Cooler description" });
                _db.SpareParts.Add(new SparePart { Name = "PowerSupply", Description = "PowerSupply description" });
                _db.SpareParts.Add(new SparePart { Name = "Riser", Description = "Riser description" });
                _db.SaveChanges();
            }
        }

        #region SpareParts implementation

        public IEnumerable<SparePart> GetAllSpareParts()
        {
            return _db.SpareParts.ToList();
        }

        public void CreateSparePart(SparePart sparePart)
        {
            _db.SpareParts.Add(sparePart);
            _db.SaveChanges();
        }

        public SparePart ReadSparePart(int id)
        {
            SparePart sparePart = _db.SpareParts.FirstOrDefault(x => x.Id == id);
            return sparePart;
        }

        public void UpdateSparePart(SparePart sparePart)
        {
            _db.SpareParts.Update(sparePart);
            _db.SaveChanges();
        }

        public void DeleteSparePart(int id)
        {
            SparePart sparePart = _db.SpareParts.FirstOrDefault(x => x.Id == id);
            if (sparePart != null)
            {
                _db.SpareParts.Remove(sparePart);
                _db.SaveChanges();
            }
        }

        public void DeleteAllSpareParts()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
