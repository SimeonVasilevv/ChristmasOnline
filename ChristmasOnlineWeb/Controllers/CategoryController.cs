using ChristmasOnline.DataAccess.Repository.IRepository;
using ChristmasOnline.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChristmasOnlineWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _unitOfWork.Category.GetAll();
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
            if (_unitOfWork.Category.GetFirstOrDefault(c => c.Name == category.Name) != null)
            {
                ModelState.AddModelError("name", $"Category \"{category.Name}\" already exists.");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
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

            Category category = _unitOfWork.Category.GetFirstOrDefault(c => c.Id==id);

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
            if (_unitOfWork.Category.GetFirstOrDefault(c => c.Name == category.Name) != null)
            {
                ModelState.AddModelError("name", $"Category \"{category.Name}\" already exists.");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
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

            Category category = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);

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
            var categoryToDelete = _unitOfWork.Category.GetFirstOrDefault(c=>c.Id==id);

            if (categoryToDelete == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(categoryToDelete);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index");
        }

    }
}
