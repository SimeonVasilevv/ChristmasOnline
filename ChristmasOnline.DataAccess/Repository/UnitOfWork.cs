using ChristmasOnline.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasOnline.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext context)
        {
            _db = context;
            Category = new CategoryRepository(_db);
            Material = new MaterialRepository(_db);
        }

        public ICategoryRepository Category { get; private set; }

        public IMaterialRepository Material { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
