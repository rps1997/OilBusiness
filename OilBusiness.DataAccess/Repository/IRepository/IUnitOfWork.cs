using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OilBusiness.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IBusinessTypeRepository BusinessType { get; }
        IProductRepository Product { get; }
        void Save();
    }
}
