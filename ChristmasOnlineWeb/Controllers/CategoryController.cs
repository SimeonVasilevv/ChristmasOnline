using ChristmasOnlineWeb.Data;
using ChristmasOnlineWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChristmasOnlineWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _db.Categories;
            return View(categoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (_db.Categories.FirstOrDefault(c=>c.Name == category.Name) != null)
            {
                ModelState.AddModelError("name", $"Category \"{category.Name}\" already exists.");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }
    }
}
