using Kent.Entities.Model;
using Kent.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Services
{
    public class EmployeesServices : IEmployeesServices
    {
        public readonly IEmployeesRepository _employeesRepository;
        public EmployeesServices(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        public List<Employees> GetList()
        {
            return _employeesRepository.GetList();
        }

        public bool AddNewEmployees(Employees employees)
        {
            return _employeesRepository.AddNewEmployees(employees);
        }
    }
}
