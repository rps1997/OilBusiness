using OilBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OilBusiness.DataAccess.Repository.IRepository
{
    public interface IBusinessTypeRepository:IRepository<BusinessType>
    {
        void Update(BusinessType obj);
    }
}
