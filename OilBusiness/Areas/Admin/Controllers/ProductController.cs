using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OilBusiness.Data;
using OilBusiness.DataAccess.Repository;
using OilBusiness.DataAccess.Repository.IRepository;
using OilBusiness.Models;
using OilBusiness.Models.ViewModels;

namespace OilBusiness.Controllers;
[Area("Admin")]

    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly ApplicationDbContext _db;
        public readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork=unitOfWork;
            _hostEnvironment=hostEnvironment;
            //_db = db;
        }
        public IActionResult Index()
        {
            //IEnumerable<Product> obj = _unitOfWork.Product.GetAll();
            return View();
        }
        //GET
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(
                        u => new SelectListItem
                        {
                            Text = u.Name,
                            Value = u.Id.ToString(),
                        }),
                BusinessTypeList = _unitOfWork.BusinessType.GetAll().Select(
                        u=> new SelectListItem
                        {
                            Text=u.Name,
                            Value=u.id.ToString(),  
                        }
                    )
            };
            if(id == null || id == 0)
            {
                //create product
                //ViewBag.CategoryList = CategoryList;
                //ViewData["BusinessTypeList"] = BusinessTypeList;
                return View(productVM);
            }
            else
            {
                productVM.Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
                return View(productVM);
                //Edit product
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if(ModelState.IsValid)
            { 
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products\");
                    var extension = Path.GetExtension(file.FileName);
                    if(obj.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using(var fileStream = new FileStream(Path.Combine(uploads, fileName+extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }
            if(obj.Product.Id == 0)
            {
                 _unitOfWork.Product.Add(obj.Product);
            }
            else
            {
                 _unitOfWork.Product.Update(obj.Product);
            }
                _unitOfWork.Save();
                //TempData["Success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties:"Category,BusinessType");
            return Json(new { data = productList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            
                var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
                if(obj!= null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    var oldImagePath = Path.Combine(wwwRootPath, obj.ImageUrl.Trim('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    _unitOfWork.Product.Remove(obj);
                    _unitOfWork.Save();
                    return Json(new { success = true });
                }   

            return View();
        }
    #endregion
}




