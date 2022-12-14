using ChristmasOnline.DataAccess.Repository.IRepository;
using ChristmasOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasOnline.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext context)
            :base(context)
        {
            _db = context;
        }


        public void Update(Category category)
        {
            _db.Categories.Update(category);
        }
    }
}
