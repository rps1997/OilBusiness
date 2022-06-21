using Microsoft.AspNetCore.Mvc;
using OilBusiness.Data;
using OilBusiness.DataAccess.Repository;
using OilBusiness.DataAccess.Repository.IRepository;
//using OilBusiness.DataAccess;
using OilBusiness.Models;

namespace OilBusiness.Controllers;
[Area("Admin")]
public class CategoryController : Controller
    {
        private readonly IUnitOfWork _db;
        //public IEnumerable<Category> Categories { get; set; }
        public CategoryController(IUnitOfWork db)
        {
            _db= db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Category.GetAll();
            return View(objCategoryList);
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
            if (ModelState.IsValid)
            {
                _db.Category.Add(category);
                _db.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        //GET
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            else
            {
                var categoryFromDb = _db.Category.GetFirstOrDefault(u => u.Id == id);
                return View(categoryFromDb);
            }
            
            //if(categoryFromDb == null)
            //{
            //    return NotFound();
            //}
            //else
            //{
            //    _db.Categories.Update(categoryFromDb);
            //    _db.SaveChanges();
            //}
            //return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            _db.Category.Update(category);
            _db.Save();
            return RedirectToAction(nameof(Index));
        }
        //GET
        public IActionResult Delete(int? id)
        {
            var categoryFromDb = _db.Category.GetFirstOrDefault(u => u.Id == id);
            //if(categoryFromDb == null)
            //{
            //    return NotFound();
            //}
            //else
            //{
            //    _db.Categories.Update(categoryFromDb);
            //    _db.SaveChanges();
            //}
            return View(categoryFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var catFromDb = _db.Category.GetFirstOrDefault(u=>u.Id==id);
            
            if(catFromDb == null)
            {
                return NotFound();
            }
            else
            {
                _db.Category.Remove(catFromDb);
                _db.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
            
        }
    }

