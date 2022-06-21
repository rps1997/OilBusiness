using Microsoft.AspNetCore.Mvc;
using OilBusiness.Data;
using OilBusiness.DataAccess.Repository.IRepository;
using OilBusiness.Models;

namespace OilBusiness.Controllers;
[Area("Admin")]
public class BusinessTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public BusinessTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<BusinessType> obj = _unitOfWork.BusinessType.GetAll();
            return View(obj);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BusinessType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.BusinessType.Add(obj);
                _unitOfWork.Save();
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
                var busFromDb = _unitOfWork.BusinessType.GetFirstOrDefault(u => u.id == id);
                return View(busFromDb);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BusinessType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.BusinessType.Update(obj);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        //GET
        public IActionResult Delete(int? id)
        {
            var busFromDb = _unitOfWork.BusinessType.GetFirstOrDefault(u => u.id == id);
            return View(busFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            
            var busFromDb = _unitOfWork.BusinessType.GetFirstOrDefault(i => i.id == id);
            if (busFromDb == null)
            {
                return NotFound();
            }
            else
            {
                _unitOfWork.BusinessType.Remove(busFromDb);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }

