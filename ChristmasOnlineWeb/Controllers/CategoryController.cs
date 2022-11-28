using ChristmasOnlineWeb.Data;
using ChristmasOnlineWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            if (_db.Categories.FirstOrDefault(c => c.Name == category.Name) != null)
            {
                ModelState.AddModelError("name", $"Category \"{category.Name}\" already exists.");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully!";
                return RedirectToAction("Index");
            }

            return View(category);
        }

        //GET
        public IActionResult Edit(string? id)
        {
            if (id == null || id == string.Empty)
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
        [ValidateAntiForgeryToken]
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
                TempData["success"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }

            return View(category);
        }

        //GET
        public IActionResult Delete(string? id)
        {
            if (id == null || id == string.Empty)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(string? id)
        {
            var categoryToDelete = _db.Categories.Find(id);

            if (categoryToDelete == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(categoryToDelete);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index");
        }

    }
}
