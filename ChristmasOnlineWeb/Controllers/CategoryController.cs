using ChristmasOnlineWeb.Data;
using ChristmasOnlineWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        //GET
        public IActionResult Edit(string? id)
        {
            if (id == null || id==string.Empty)
            {
                return NotFound();
            }

            Category category = _db.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //POST
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (_db.Categories.FirstOrDefault(c => c.Name == category.Name) != null)
            {
                ModelState.AddModelError("name", $"Category \"{category.Name}\" already exists.");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }
    }
}
