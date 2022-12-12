using ChristmasOnline.DataAccess.Repository.IRepository;
using ChristmasOnline.Models;

namespace ChristmasOnline.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext context)
            :base(context)
        {
           _db = context;
        }

        public void Update(Product product)
        {
            var productFromDb = _db.Products.FirstOrDefault(x => x.Id == product.Id);

            if (productFromDb != null)
            {
                productFromDb.Barcode = product.Barcode;
                productFromDb.ListPrice = product.ListPrice;
                productFromDb.Price = product.Price;
                productFromDb.Price20 = product.Price20;
                productFromDb.Price10 = product.Price10;
                productFromDb.Name = product.Name;
                productFromDb.Description = product.Description;
                productFromDb.CategoryId = product.CategoryId;
                productFromDb.MaterialId = product.MaterialId;

                if (product.ImageUrl != null)
                {
                    productFromDb.ImageUrl = product.ImageUrl;
                }

            }
        }
    }
}
