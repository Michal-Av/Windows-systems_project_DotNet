using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    internal class Dal_imp : IDAL
    {
        //IDAL dal;
        public Dal_imp()
        {
            //dal = DAL.FactoryDal.GetDal();
            DataSource.specialList = new List<Specialization>();
            DataSource.employerList = new List<Employer>();
            DataSource.employeeList = new List<Employee>();
            DataSource.contractList = new List<contract>();
        }

        #region specialization functions
        void IDAL.addSeciality(Specialization s)
        {
            int index = DataSource.specialList.FindIndex(x => (x.ExpertName == s.ExpertName) && (x.DomainName == s.DomainName));
            if (index == -1)
            {
                if (s.Expert == 0)
                    s.Expert = Specialization.KodExpert++;
                else if (s.Expert < 10000000 && s.Expert > 100000000)
                    s.Expert = Specialization.KodExpert++;
                DataSource.specialList.Add(s);
            }
            else
                if (s.Expert == 0)
            {
                s.Expert = DataSource.specialList[index].Expert;
                throw new Exception("specialization num: "+s.Expert);
            }
            else
                throw new Exception("specialization already exist");

        }
        void IDAL.removeSeciality(Specialization s)
        {
            int index = DataSource.specialList.FindIndex(x => x.Expert == s.Expert);
            if (index == -1)
                throw new Exception("specialization not exist");
            DataSource.specialList.Remove(s);
        }
        void IDAL.updateSeciality(Specialization s)
        {
            int index = DataSource.specialList.FindIndex(x => x.Expert == s.Expert);
            if (index == -1)
                throw new Exception("specialization not exist");
            DataSource.specialList[index] = s;
        }
        public IEnumerable<Specialization> GetAllSpecialily(Func<Specialization, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.specialList.AsEnumerable();
            return DataSource.specialList.Where(predicate);
        }
        Specialization IDAL.GetSpecialization(int expert)
        {
            return DataSource.specialList.FirstOrDefault(x => x.Expert == expert);
        }
        Specialization IDAL.GetSpecialization(Domain d, string _name)
        {
            return DataSource.specialList.FirstOrDefault(x => x.DomainName == d);
        }
        #endregion

        #region employer functions
        void IDAL.addEmployer(Employer e)
        {
            int index = DataSource.employerList.FindIndex(x => x.CompanyNum == e.CompanyNum);
            if (index != -1)
                throw new Exception("employer already exist");
            DataSource.employerList.Add(e);
        }
        void IDAL.removeEmployer(Employer e)
        {
            int index = DataSource.employerList.FindIndex(x => x.CompanyNum == e.CompanyNum);
            if (index == -1)
                throw new Exception("employer not exist");
            DataSource.employerList.Remove(e);
        }
        void IDAL.updateEmployer(Employer e)
        {
            int index = DataSource.employerList.FindIndex(x => x.CompanyNum == e.CompanyNum);
            if (index == -1)
                throw new Exception("employer not exist");
            DataSource.employerList[index] = e;
        }
        public IEnumerable<Employer> GetAllEmployer(Func<Employer, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.employerList.AsEnumerable();
            return DataSource.employerList.Where(predicate);
        }
        Employer IDAL.GetEmployer(int id)
        {
            return DataSource.employerList.FirstOrDefault(x => x.CompanyNum == id);
        }
        #endregion

        #region employee functions
        void IDAL.addEmployee(Employee e)
        {
            //check if employee exist already
            int index = DataSource.employeeList.FindIndex(x => x.Id == e.Id);
            if (index != -1)
                throw new Exception("employee already exist");
            else
            ////check if we need to give numExpert or it exists
            //if (e.CheckExpert == 0)
            //    e.CheckExpert = Specialization.KodExpert++;
            //else
            //{
            //    int i = DataSource.specialList.FindIndex(x => x.Expert == e.CheckExpert);
            //    if (i != -1)
            //        e.CheckExpert = Specialization.KodExpert++;
            //}
            
            DataSource.employeeList.Add(e);
        }
        void IDAL.removeEmployee(Employee e)
        {
            int index = DataSource.employeeList.FindIndex(x => x.Id == e.Id);
            if (index == -1)
                throw new Exception("employee not exist");
            DataSource.employeeList.Remove(e);
        }
        void IDAL.updateEmployee(Employee e)
        {
            int index = DataSource.employeeList.FindIndex(x => x.Id == e.Id);
            if (index == -1)
                throw new Exception("employee not exist");
            DataSource.employeeList[index] = e;
        }
        public IEnumerable<Employee> GetAllEmployee(Func<Employee, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.employeeList.AsEnumerable();
            return DataSource.employeeList.Where(predicate);
        }
        Employee IDAL.GetEmployee(int id)
        {
            return DataSource.employeeList.FirstOrDefault(x => x.Id == id);
        }

        #endregion

        #region contract functions
        void IDAL.addContract(contract c)
        {
            int index = DataSource.contractList.FindIndex(x => x.IdEmployee == c.IdEmployee
            && x.IdEmployer == c.IdEmployer && x.StartDate == c.StartDate);
            if (index != -1)
                throw new Exception("contract already exist");
            c.NumContract = contract.KodContract++;
            DataSource.contractList.Add(c);
        }
        void IDAL.removeContract(contract c)
        {
            int index = DataSource.contractList.FindIndex(x => x.NumContract == c.NumContract);
            if (index == -1)
                throw new Exception("contract not exist");
            DataSource.contractList.Remove(c);
        }
        void IDAL.updateContract(contract c)
        {
            int index = DataSource.contractList.FindIndex(x => x.NumContract == c.NumContract);
            if (index == -1)
                throw new Exception("contract not exist");
            DataSource.contractList[index] = c;
         }
        void IDAL.updateCommission(contract c)
        {
            int commission = CountContractsEmployer(c) + CountContractsEmployee(c);
            if (commission == 2)
                c.NetoSalary = c.BrutoSalary - (c.BrutoSalary * 0.07);
            else
            if (commission >= 3)
                c.NetoSalary = c.BrutoSalary - (c.BrutoSalary * 0.05);
            else c.NetoSalary = c.BrutoSalary - (c.BrutoSalary * 0.1);
        }
        //contract IDAL.GetContract(string _City)
        //{
        //    return DataSource.contractList.FirstOrDefault(x => x.City == _City);
        //}
        //contract IDAL.GetContract(int _IdEmployee)
        //{
        //    return DataSource.contractList.FirstOrDefault(x => x.IdEmployee == _IdEmployee);
        //}
        contract IDAL.GetContract_Num(int num)
        {
            return DataSource.contractList.FirstOrDefault(x => x.NumContract == num);
        }
        public IEnumerable<contract> GetAllContract(Func<contract, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.contractList.AsEnumerable();
            return DataSource.contractList.Where(predicate);
        }
        #endregion
        public int CountContractsEmployee(contract c)
        {
            return GetAllContract(i => i.IdEmployee == c.IdEmployee).Count();
        }
        public int CountContractsEmployer(contract c)
        {
            return GetAllContract(i => i.IdEmployer == c.IdEmployer).Count();
        }
      
        public IEnumerable<ATM> GetAllAtm()
        {
            List<ATM> list = new List<ATM>(10);

            ATM b1 = new ATM();
            b1.ATMAddress = "Najara 26";
            b1.ATMCode = 122;
            b1.BankCode = 43;
            b1.BankName = "discont";
            b1.City = "Jerusalem";
            list.Add(b1);
            return list;
        }
    }
}
