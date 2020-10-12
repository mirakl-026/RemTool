using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RemTool.Models;
using RemTool.Infrastructure.Interfaces.Services;
using RemTool.DAL.Context.SQLExpress;

namespace RemTool.Services.SQLExpress
{
    public class ToolService : IToolService
    {
        public RemToolContext _db;

        public ToolService(RemToolContext db) 
        {
            _db = db;
            if (!_db.Tools.Any())
            {
                _db.Tools.Add(new Tool { Name = "Lamp F1000", Description = "Lamp F1000 description" });
                _db.Tools.Add(new Tool { Name = "Saw X101", Description = "Saw X101 description" });
                _db.Tools.Add(new Tool { Name = "Bolgarka-1", Description = "Bolgarka-1 description" });
                _db.SaveChanges();
            }
        }

        #region Tools implementation

        public IEnumerable<Tool> GetAllTools()
        {
            return _db.Tools.ToList();
        }

        public void CreateTool(Tool tool)
        {
            _db.Tools.Add(tool);
            _db.SaveChanges();
        }

        public Tool ReadTool(int id)
        {
            Tool tool = _db.Tools.FirstOrDefault(x => x.Id == id);
            return tool;
        }

        public void UpdateTool(Tool tool)
        {
            _db.Tools.Update(tool);
            _db.SaveChanges();
        }

        public void DeleteTool(int id)
        {
            Tool tool = _db.Tools.FirstOrDefault(x => x.Id == id);
            if (tool != null)
            {
                _db.Tools.Remove(tool);
                _db.SaveChanges();
            }
        }

        public void DeleteAllTools()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
