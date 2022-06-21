using OilBusiness.Data;
using OilBusiness.DataAccess.Repository.IRepository;
using OilBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OilBusiness.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Price = obj.Price;
                objFromDb.Price25 = obj.Price25;
                objFromDb.Price50 = obj.Price50;
                if (obj.ImageUrl != null)
                { 
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.BusinessTypeId = obj.BusinessTypeId;
               
            }
            
        }
    }
}
