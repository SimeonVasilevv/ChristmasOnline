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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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
        public IActionResult Upsert(ProductViewModel product, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString(); //Guid в случай, че се качат 2 файла с еднакви имена
                    var upload = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName); // След преименуване, запазваме extension-а на файла.

                    using (var fileStream = new FileStream(Path.Combine(upload,fileName+extension),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    product.ImageUrl = @"\images\products\" + fileName + extension;
                }

                var productToAdd = new Product
                {
                    Name = product.Name,
                    Price = product.Price,
                    Price10 = product.Price10,
                    Price20 = product.Price20,
                    ListPrice = product.ListPrice,
                    Barcode = product.Barcode,
                    Category = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == product.CategoryId),
                    Material = _unitOfWork.Material.GetFirstOrDefault(m => m.Id == product.MaterialId),
                    Description = product.Description,
                    DateOfReceiving = product.DateOfReceiving,
                    ImageUrl = product.ImageUrl
                };
                _unitOfWork.Product.Add(productToAdd);
                _unitOfWork.Save();
                TempData["success"] = "Product successfully created!";
                return RedirectToAction("Index");
            }

            return View(product);
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
