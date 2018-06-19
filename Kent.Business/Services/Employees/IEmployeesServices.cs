using Kent.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Services
{
    public interface IEmployeesServices
    {
        List<Employees> GetList();
        bool AddNewEmployees(Employees employees);
    }
}
