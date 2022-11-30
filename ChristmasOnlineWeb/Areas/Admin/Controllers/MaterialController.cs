using ChristmasOnline.DataAccess.Repository.IRepository;
using ChristmasOnline.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChristmasOnlineWeb.Areas.Admin.Controllers
{
    public class MaterialController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MaterialController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Material> materialList = _unitOfWork.Material.GetAll();
            return View(materialList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Material material)
        {
            if (_unitOfWork.Category.GetFirstOrDefault(c => c.Name == material.Name) != null)
            {
                ModelState.AddModelError("name", $"Material \"{material.Name}\" already exists.");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Material.Add(material);
                _unitOfWork.Save();
                TempData["success"] = "Material created successfully!";
                return RedirectToAction("Index");
            }

            return View(material);
        }

        //GET
        public IActionResult Edit(string? id)
        {
            if (id == null || id == string.Empty)
            {
                return NotFound();
            }

            Material material = _unitOfWork.Material.GetFirstOrDefault(c => c.Id == id);

            if (material == null)
            {
                return NotFound();
            }
            return View(material);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Material material)
        {
            if (_unitOfWork.Material.GetFirstOrDefault(c => c.Name == material.Name) != null)
            {
                ModelState.AddModelError("name", $"Material \"{material.Name}\" already exists.");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Material.Update(material);
                _unitOfWork.Save();
                TempData["success"] = "Material updated successfully!";
                return RedirectToAction("Index");
            }

            return View(material);
        }

        //GET
        public IActionResult Delete(string? id)
        {
            if (id == null || id == string.Empty)
            {
                return NotFound();
            }

            Material material = _unitOfWork.Material.GetFirstOrDefault(c => c.Id == id);

            if (material == null)
            {
                return NotFound();
            }
            return View(material);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(string? id)
        {
            var materialToDelete = _unitOfWork.Material.GetFirstOrDefault(c => c.Id == id);

            if (materialToDelete == null)
            {
                return NotFound();
            }

            _unitOfWork.Material.Remove(materialToDelete);
            _unitOfWork.Save();
            TempData["success"] = "Material deleted successfully!";
            return RedirectToAction("Index");
        }

    }
}
