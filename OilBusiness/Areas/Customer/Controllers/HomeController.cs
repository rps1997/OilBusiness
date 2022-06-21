using Microsoft.AspNetCore.Mvc;
using OilBusiness.DataAccess.Repository.IRepository;
using OilBusiness.Models;
using OilBusiness.Models.ViewModels;
using System.Diagnostics;

namespace OilBusiness.Controllers;
[Area("Customer")]
public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> obj = _unitOfWork.Product.GetAll(includeProperties: "Category,BusinessType");
            return View(obj);
        }
        //GET
        public IActionResult Details(int? id)
        {
        if(id == null)
        {
            return NotFound();
        }
        ShoppingCart cartObj = new()
        {
            Count = 1,
            Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category,BusinessType")
        };
        return View(cartObj);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
