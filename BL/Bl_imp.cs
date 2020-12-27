using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{
    public class Bl_imp : IBL
    {
        DAL.IDAL dal;
        BL.IBL bl;
        public Bl_imp()
        {
            dal = DAL.FactoryDal.GetDal();
        }
        protected static Bl_imp instance = null;
        public static Bl_imp GetInstance()
        {
            if (instance == null)
                instance = new Bl_imp();
            return instance;
        }
        #region specialization functions
        void IBL.addSeciality(Specialization s)
        {
            dal.addSeciality(s);
        }
        void IBL.removeSeciality(Specialization s)
        {
            try
            {
                dal.removeSeciality(s);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        void IBL.updateSeciality(Specialization s)
        {
            try
            {
                dal.updateSeciality(s);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        IEnumerable<Specialization> IBL.GetAllSpecialily(Func<Specialization, bool> predicate)
        {
            return dal.GetAllSpecialily(predicate);
        }
        Specialization IBL.GetSpecialization(int expert)
        {
            return dal.GetSpecialization(expert);
        }
        #endregion

        #region employer functions
        void IBL.addEmployer(Employer e)
        {
            try
            {
                dal.addEmployer(e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        void IBL.removeEmployer(Employer e)
        {
            try
            {
                var v = dal.GetAllContract(x => x.IdEmployer == e.CompanyNum);
            int count = v.Count();
                if (count != 0)
                    foreach (var item in v)
                    {
                        if (count != 0)
                        {
                            dal.removeContract(item);
                            count--;
                        }
                        break;
                    }
                dal.removeEmployer(e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        void IBL.updateEmployer(Employer e)
        {
            try
            {
                dal.updateEmployer(e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        IEnumerable<Employer> IBL.GetAllEmployer(Func<Employer, bool> predicate)
        {
            return dal.GetAllEmployer(predicate);
        }
        Employer IBL.GetEmployer(int id)
        {
            return dal.GetEmployer(id);
        }

        #endregion

        #region employee functions
        void IBL.addEmployee(Employee e)
        {
            if ((DateTime.Now.Year) - (e.Date.Year) < 18)
                throw new Exception("This employee is under 18 years old");
            try
            {
                dal.addEmployee(e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        void IBL.removeEmployee(Employee e)
        {
            try
            {
                var v = dal.GetAllContract(x => x.IdEmployee == e.Id);
                int count = v.Count();
                if (count != 0)
                    foreach (var item in v)
                    {
                        if (count != 0)
                        {
                            dal.removeContract(item);
                            count--;
                        }
                        break;
                    }

                dal.removeEmployee(e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void IBL.updateEmployee(Employee e)
        {
            try
            {
                dal.updateEmployee(e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        Employee IBL.GetEmployee(int id)
        {
            return dal.GetEmployee(id);
        }
        IEnumerable<Employee> IBL.GetAllEmployee(Func<Employee, bool> predicate)
        {
            return dal.GetAllEmployee(predicate);
        }

        #endregion

        #region contract functions
        void IBL.addContract(contract c)
        {
            dal.updateCommission(c);
            bool flag1 = false;
            bool flag2 = false;
            DateTime today = DateTime.Now;
            foreach (Employer i in dal.GetAllEmployer())
                if ((i.CompanyNum == c.IdEmployer) && (today.Year - i.establish.Year >= 1))
                    flag1 = true;
            foreach (Employee item in dal.GetAllEmployee())
                if (item.Id == c.IdEmployee)
                    flag2 = true;
            if (flag1 && flag2)
            {
                try
                {
                    dal.addContract(c);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
                throw new Exception("Unable to open contract");

        }
        void IBL.removeContract(contract c)
        {
            try
            {
                dal.removeContract(c);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        void IBL.updateContract(contract c)
        {
            try
            {
                dal.updateContract(c);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        contract IBL.GetContract_Num(int num)
        {
            return dal.GetContract_Num(num);
        }
        IEnumerable<contract> IBL.GetAllContract(Func<contract, bool> predicate)
        {
            return dal.GetAllContract(predicate);
        }
        int IBL.GetNumContract(Func<contract, bool> predicate)
        {
            return dal.GetAllContract().Where(predicate).Count();
        }
        #endregion

        #region Grouping
        IEnumerable<IGrouping<string, contract>> IBL.GroupContractByCity(bool sort = false)
        {
            if (sort)
                return from item in dal.GetAllContract()
                       from e in dal.GetAllEmployee()
                       orderby item.NumContract
                       where item.IdEmployee == e.Id
                       group item by e.City;
            else
                return from item in dal.GetAllContract()
                       from e in dal.GetAllEmployee()
                       where item.IdEmployee == e.Id
                       group item by e.City;
        }
        IEnumerable<IGrouping<string, contract>> IBL.GroupContractBySpecialization(bool sort = false)
        {
            if (sort)
                return from c in dal.GetAllContract()
                       orderby c.NumContract
                       from e in dal.GetAllEmployee()
                       where c.IdEmployee == e.Id
                       group c by e.NumSpecialization;
            else
                return from c in dal.GetAllContract()
                       from e in dal.GetAllEmployee()
                       where c.IdEmployee == e.Id
                       group c by e.NumSpecialization;
        }
        IEnumerable<IGrouping<int, double>> IBL.GroupBenefitByMonth(bool sort = false)
        {
            if (sort)
                return from item in dal.GetAllContract()
                       let Commission = item.MonthHours * (item.BrutoSalary - item.NetoSalary)
                       orderby Commission
                       group Commission by item.StartDate.Month;
            else
                return from item in dal.GetAllContract()
                       let Commission = item.MonthHours * (item.BrutoSalary - item.NetoSalary)
                       group Commission by item.StartDate.Month;
        }
        #endregion

        #region my functions
        IEnumerable<contract> IBL.GetAllOldContract()
        {
            return from item in dal.GetAllContract(x => x.EndDate.Month == DateTime.Now.Month)
                   where (item.EndDate.Year == DateTime.Now.Year)
                   select item;
        }
        IEnumerable<Employee> IBL.GetAllEmployeeLastName()
        {
            return from item in dal.GetAllEmployee()
                   orderby item.LastName
                   select item;
        }
        IEnumerable<Employer> IBL.GetAllEmployerLastName()
        {
            return from item in dal.GetAllEmployer()
                   orderby item.LastName
                   select item;
        }
        int IBL.GetNumPrivateBusiness()
        {
            //private=true   company=false
            return dal.GetAllEmployer(x => x.EmployerStatus).Count();
        }
        IEnumerable<Employee> IBL.GetAllFarEmployee()
        {
            IEnumerable<Employee> v;
            v = from item in dal.GetAllContract()
                from i in dal.GetAllEmployee()
                where (item.IdEmployee==i.Id)&&(item.WorkAddress != i.City)
                select i;
            return v;
        }
        IEnumerable<Employee> IBL.GetAllEndContractStudent()
        {
            return from item in dal.GetAllContract(x => (x.EndDate.Month == (DateTime.Now.Month)+1)
                   &&(x.EndDate.Year == (DateTime.Now.Year)))
                   where (dal.GetEmployee(item.IdEmployee).Education == education.Student)
                   select dal.GetEmployee(item.IdEmployee);

        }
        #endregion

        #region Banks Groups
        public static IEnumerable<IGrouping<string, ATM>> GetAllAtmGroupByCity()
        {
            return from atm in Dal_imp_XML.GetAllAtm()
                   group atm by atm.City;
        }

        public static IEnumerable<string> GetAllBankNames()
        {
            return (from atm in Dal_imp_XML.GetAllAtm()
                    select atm.BankName).Distinct();
        }

        public static IEnumerable<ATM> GetAllBankAtm(string bankName)
        {
            return (from atm in Dal_imp_XML.GetAllAtm()
                    where atm.BankName.CompareTo(bankName) == 0
                    select atm).Distinct();
        }

        public static IEnumerable<IGrouping<string, IGrouping<string, ATM>>> GetAllAtmGroupByBankAndCity()
        {
            var queryNestedGroups =
                from atm in Dal_imp_XML.GetAllAtm().Distinct()
                orderby atm.BankName, atm.City, atm.BankCode // optional for sort list
                group atm by atm.BankName into g  // group by bank name
                from newGroup2 in (from atm2 in g   // foreach bank name grouping now group by city
                                   group atm2 by atm2.City)
                group newGroup2 by g.Key;

            return queryNestedGroups;
        }
        #endregion
    }
}
