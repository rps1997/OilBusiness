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
    public class BusinessTypeRepository : Repository<BusinessType>, IBusinessTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public BusinessTypeRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(BusinessType obj)
        {
            _db.businessTypes.Update(obj);
        }
    }
}
