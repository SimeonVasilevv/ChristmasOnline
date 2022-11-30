using ChristmasOnline.DataAccess.Repository.IRepository;
using ChristmasOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasOnline.DataAccess.Repository
{
    public class MaterialRepository : Repository<Material>, IMaterialRepository
    {
        private ApplicationDbContext _db;

        public MaterialRepository(ApplicationDbContext context)
            :base(context)
        {
           _db = context;
        }

        public void Update(Material material)
        {
            _db.Materials.Update(material);
        }
    }
}
