using Kent.Entities.Model;
using System;
using System.Collections.Generic;

namespace Kent.Entities.Repositories
{
    public interface IEmployeesRepository 
    {
        List<Employees> GetList();
        bool AddNewEmployees(Employees employees);
    }
}
