using ChristmasOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasOnline.DataAccess.Repository.IRepository
{
    public interface IMaterialRepository : IRepository<Material>
    {
        void Update(Material material);
    }
}
