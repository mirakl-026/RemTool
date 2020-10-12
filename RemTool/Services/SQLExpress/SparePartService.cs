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
        private readonly RemToolContext _db;

        public SparePartService(RemToolContext db) => _db = db;

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
