using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface IDAL
    {
        void addSeciality(Specialization s);
        void removeSeciality(Specialization s);
        void updateSeciality(Specialization s);
        Specialization GetSpecialization(int expert);
        Specialization GetSpecialization(Domain d, string _name);

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
        //contract GetContract(string _city);
        //contract GetContract(int _IdEmployee);
        contract GetContract_Num(int num);
        int CountContractsEmployee(contract c);
        int CountContractsEmployer(contract c);
        void updateCommission(contract c);

        IEnumerable<Specialization> GetAllSpecialily(Func<Specialization, bool> predicate = null);
        IEnumerable<Employer> GetAllEmployer(Func<Employer, bool> predicate = null);
        IEnumerable<Employee> GetAllEmployee(Func<Employee, bool> predicate = null);
        IEnumerable<contract> GetAllContract(Func<contract, bool> predicate = null);
        //static IEnumerable<ATM> GetAllAtm();
        //IEnumerable<branch> GetAllBranch();
    }
}
