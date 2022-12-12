using ChristmasOnline.DataAccess.Repository.IRepository;

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
            Product = new ProductRepository(_db);
        }

        public ICategoryRepository Category { get; private set; }

        public IMaterialRepository Material { get; private set; }

        public IProductRepository Product { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
