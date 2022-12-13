using ChristmasOnline.DataAccess.Repository.IRepository;
using ChristmasOnline.Models;
using ChristmasOnline.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChristmasOnlineWeb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //IEnumerable<Category> categoryList = _unitOfWork.Category.GetAll();
            return View();
        }

        //GET
        public IActionResult Upsert(string? id)
        {
            ProductViewModel productViewModel = new()
            {
                Product = new(),
               
                CategoryList = _unitOfWork.Category.GetAll()
                .Select(
                    c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id
                    }),
               
                MaterialList = _unitOfWork.Material.GetAll()
                .Select(
                    m => new SelectListItem
                    {
                        Text = m.Name,
                        Value = m.Id
                    })
            };

            if (id == null || id == string.Empty)
            {
                return View(productViewModel);
            }

            else
            {

            }
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
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
            var categoryToDelete = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);

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
