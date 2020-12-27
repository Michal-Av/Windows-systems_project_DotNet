using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{
    public interface IBL
    {
        
        void addSeciality(Specialization s);
        void removeSeciality(Specialization s);
        void updateSeciality(Specialization s);
        Specialization GetSpecialization(int expert);

        void addEmployer(Employer e);
        void removeEmployer(Employer e);
        void updateEmployer(Employer e);
        Employer GetEmployer(int id);

        void addEmployee(Employee e);
        void removeEmployee(Employee e);
        void updateEmployee(Employee e);
        Employee GetEmployee(int id);
        
        void addContract(contract c);
        void removeContract(contract c);
        void updateContract(contract c);
        //contract GetContract(string _City);
        //contract GetContract(int _IdEmployee);
        contract GetContract_Num(int num);

        IEnumerable<Specialization> GetAllSpecialily(Func<Specialization, bool> predicate = null);
        IEnumerable<Employer> GetAllEmployer(Func<Employer, bool> predicate = null);
        IEnumerable<Employee> GetAllEmployee(Func<Employee, bool> predicate = null);
        IEnumerable<contract> GetAllContract(Func<contract, bool> predicate = null);
        IEnumerable<contract> GetAllOldContract();
        IEnumerable<Employee> GetAllEmployeeLastName();
        IEnumerable<Employer> GetAllEmployerLastName();
        int GetNumContract(Func<contract, bool> predicate = null);
        IEnumerable<IGrouping<string, contract>> GroupContractByCity(bool sort = false);
        IEnumerable<IGrouping<string, contract>> GroupContractBySpecialization(bool sort = false);
        IEnumerable<IGrouping<int , double>> GroupBenefitByMonth(bool sort = false);
        int GetNumPrivateBusiness();
        //IEnumerable<IGrouping<int, Employee>> GroupAllFarEmployee()
        IEnumerable<Employee> GetAllFarEmployee();
        IEnumerable<Employee> GetAllEndContractStudent();
    }

}
