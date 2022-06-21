using OilBusiness.Data;
using OilBusiness.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OilBusiness.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            BusinessType = new BusinessTypeRepository(_db);
            Product=new ProductRepository(_db);
        }
        public ICategoryRepository Category { get;private set; }
        public IBusinessTypeRepository BusinessType { get;private set; }
        public IProductRepository Product { get;private set; }  

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
