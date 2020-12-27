using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.ComponentModel;
using System.Reflection;
using DS;
using BE;

namespace DAL
{
    public class Dal_imp_XML : IDAL
    {
        IDAL daloosh = new Dal_imp();
        const string employeePath = @"EmployeeXml.xml";
        const string employerPath = @"EmployerXml.xml";
        const string specializationPath = @"specializationXml.xml";
        const string contractPath = @"contractXml.xml";
        const string configPath = @"Config.xml";

        public int kodExpert { get; set; }
        public int kodContract { get; set; }

        public Dal_imp_XML()//open XML files
        {
            try
            {
                if (!File.Exists(employeePath))
                {
                    DataSource_Xml.employeeRoot = new XElement("EMPLOYEES");
                    DataSource_Xml.employeeRoot.Save(employeePath);
                }
                else DataSource_Xml.employeeRoot = XElement.Load(employeePath);
            }
            catch { throw new Exception("Employee File Upload Problem"); }
            try
            {
                if (!File.Exists(employerPath))
                {
                    DataSource_Xml.employerRoot = new XElement("EMPLOYERS");
                    DataSource_Xml.employerRoot.Save(employerPath);
                }
                else DataSource_Xml.employerRoot = XElement.Load(employerPath);
            }
            catch { throw new Exception("Employer File Upload Problem"); }
            try
            {
                if (!File.Exists(specializationPath))
                {
                    DataSource_Xml.specializationRoot = new XElement("SPECIALIZATIONS");
                    DataSource_Xml.specializationRoot.Save(specializationPath);
                }
                else DataSource_Xml.specializationRoot = XElement.Load(specializationPath);
            }
            catch { throw new Exception("specialization File Upload Problem"); }
            try
            {
                if (!File.Exists(contractPath))
                {
                    DataSource_Xml.contractRoot = new XElement("CONTRACTS");
                    DataSource_Xml.contractRoot.Save(contractPath);
                }
                else DataSource_Xml.contractRoot = XElement.Load(contractPath);
            }
            catch { throw new Exception("Contract File Upload Problem"); }
            try
            {
                if (!File.Exists(configPath))
                {
                    DataSource_Xml.configRoot = new XElement("CONFIG", 10000000);
                    DataSource_Xml.configRoot.Add(new XElement("KodExpert", 10000000),
                                                  new XElement("KodContract", 10000000));
                    DataSource_Xml.configRoot.Save(configPath);
                }
                else DataSource_Xml.configRoot = XElement.Load(configPath);
            }
            catch { throw new Exception("Config File Upload Problem"); }

        }
        #region download from internet
        const string xmlLocalPath = @"atm.xml";
        string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
        private static void DownloadATM()
        {
            const string xmlLocalPath = @"atm.xml";
            WebClient wc = new WebClient();
            try
            {
                string xmlServerPath = @"http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
            }
            catch (Exception)
            {
                string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
            }
            finally
            {
                wc.Dispose();
            }
        }
        public static IEnumerable<ATM> GetAllAtm()
        {
            DownloadATM();
            XElement xml = XElement.Load(xmlLocalPath);

            foreach (var item in xml.Elements())
            {
                yield return new ATM
                {
                    BankCode = int.Parse(item.Element("קוד_בנק").Value),
                    ATMCode = int.Parse(item.Element("קוד_סניף").Value),
                    BankName = item.Element("שם_בנק").Value.Replace('\n', ' ').Trim(),
                    ATMAddress = item.Element("כתובת_ה-ATM").Value.Replace('\n', ' ').Trim(),
                    City = item.Element("ישוב").Value.Replace('\n', ' ').Trim(),
                };
            }
        }
        #endregion

        #region ConvertEmployee
        XElement ConvertEmployee(BE.Employee employee)
        {
            XElement employeeElement = new XElement("employee");
            foreach (PropertyInfo item in typeof(BE.Employee).GetProperties())
                employeeElement.Add(new XElement(item.Name, item.GetValue(employee, null)));
            return employeeElement;
        }

        BE.Employee ConvertEmployee(XElement element)
        {
            Employee employee = new Employee();
            foreach (PropertyInfo item in typeof(BE.Employee).GetProperties())
            {
                TypeConverter typeConverter = TypeDescriptor.GetConverter(item.PropertyType);
                object convertValue = typeConverter.ConvertFromString(element.Element(item.Name).Value);
                item.SetValue(employee, convertValue);
            }
            return employee;
        }
        #endregion

        #region ConvertEmployer
        XElement ConvertEmployer(BE.Employer employer)
        {
            XElement employerElement = new XElement("employer");
            foreach (PropertyInfo item in typeof(BE.Employer).GetProperties())
                employerElement.Add(new XElement(item.Name, item.GetValue(employer, null)));
            return employerElement;
        }

        BE.Employer ConvertEmployer(XElement element)
        {
            Employer employer = new Employer();
            foreach (PropertyInfo item in typeof(BE.Employer).GetProperties())
            {
                TypeConverter typeConverter = TypeDescriptor.GetConverter(item.PropertyType);
                object convertValue = typeConverter.ConvertFromString(element.Element(item.Name).Value);
                item.SetValue(employer, convertValue);
            }
            return employer;
        }
        #endregion

        #region ConvertSpecialization
        XElement ConvertSpecialization(BE.Specialization specialization)
        {
            XElement specializationElement = new XElement("specialization");
            foreach (PropertyInfo item in typeof(Specialization).GetProperties())
                specializationElement.Add(new XElement(item.Name, item.GetValue(specialization, null)));
            return specializationElement;
        }

        BE.Specialization ConvertSpecialization(XElement element)
        {
            Specialization specialization = new Specialization();
            foreach (PropertyInfo item in typeof(BE.Specialization).GetProperties())
            {
                TypeConverter typeConverter = TypeDescriptor.GetConverter(item.PropertyType);
                object convertValue = typeConverter.ConvertFromString(element.Element(item.Name).Value);
                item.SetValue(specialization, convertValue);
            }
            return specialization;
        }
        #endregion

        #region ConvertContract
        XElement ConvertContract(BE.contract Contract)
        {
            XElement ContractElement = new XElement("Contract");
            foreach (PropertyInfo item in typeof(BE.contract).GetProperties())
                ContractElement.Add(new XElement(item.Name, item.GetValue(Contract, null)));
            return ContractElement;
        }

        BE.contract ConvertContract(XElement element)
        {
            contract Contract = new contract();
            foreach (PropertyInfo item in typeof(BE.contract).GetProperties())
            {
                TypeConverter typeConverter = TypeDescriptor.GetConverter(item.PropertyType);
                object convertValue = typeConverter.ConvertFromString(element.Element(item.Name).Value);
                item.SetValue(Contract, convertValue);
            }
            return Contract;
        }
        #endregion


        #region employee functions
        void IDAL.addEmployee(Employee e)
        {
            if (e.Id > 100000000 && e.Id < 1000000000)
            {
                if ((e.End).Length == 7)
                {
                    Employee emp = GetEmployee(e.Id);
                    if (emp != null)
                        throw new Exception("employee already exist");
                    DataSource_Xml.employeeRoot.Add(new XElement("employee", new XElement("Id", e.Id),
                                                                                      new XElement("LastName", e.LastName),
                                                                                      new XElement("FirstName", e.FirstName),
                                                                                      new XElement("Date", e.Date),
                                                                                      new XElement("PhoneNumber", e.PhoneNumber),
                                                                                      new XElement("Start", e.Start),
                                                                                      new XElement("End", e.End),
                                                                                      new XElement("Address", e.Address),
                                                                                      new XElement("City", e.City),
                                                                                      new XElement("Education", e.Education),
                                                                                      new XElement("ImageSource", e.ImageSource),
                                                                                      new XElement("Army", e.Army),
                                                                                      new XElement("NumSpecialization", e.NumSpecialization)));
                    DataSource_Xml.employeeRoot.Save(employeePath);
                }
                else throw new Exception("Check your Phone Number again");
            }
            else throw new Exception("Check your Id again");
        }
        void IDAL.removeEmployee(Employee e)
        {
            if (e.Id > 100000000 && e.Id < 1000000000)
            {
                if ((e.End).Length == 7)
                {
                    XElement emp = (from item in DataSource_Xml.employeeRoot.Elements()
                                    where int.Parse(item.Element("Id").Value) == e.Id
                                    select item).FirstOrDefault();
                    if (emp == null)
                        throw new Exception("employee not exist");
                    emp.Remove();
                    DataSource_Xml.employeeRoot.Save(employeePath);
                }
                else throw new Exception("Check your Phone Number again");
            }
            else throw new Exception("Check your Id again");
        }
        void IDAL.updateEmployee(Employee e)
        {
            if (e.Id > 100000000 && e.Id < 1000000000)
            {
                if ((e.End).Length == 7)
                {
                    XElement emp = (from item in DataSource_Xml.employeeRoot.Elements()
                                    where int.Parse(item.Element("Id").Value) == e.Id
                                    select item).FirstOrDefault();
                    if (emp == null)
                        throw new Exception("employee not exist");
                    foreach (PropertyInfo item in typeof(Employee).GetProperties())
                        emp.Element(item.Name).SetValue(item.GetValue(e));
                    DataSource_Xml.employeeRoot.Save(employeePath);
                }
                else throw new Exception("Check your Phone Number again");
            }
            else throw new Exception("Check your Id again");
        }
        public Employee GetEmployee(int id)
        {
            XElement emp = null;
            try
            {
                emp = (from item in DataSource_Xml.employeeRoot.Elements()
                       where int.Parse(item.Element("Id").Value) == id
                       select item).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
            if (emp == null)
                return null;
            return ConvertEmployee(emp);
        }

        #endregion

        #region employer functions
        void IDAL.addEmployer(Employer e)
        {
            if (e.CompanyNum > 100000000 && e.CompanyNum < 1000000000)
            {
                if ((e.End).Length == 7)
                {
                    Employer emp = GetEmployer(e.CompanyNum);
                    if (emp != null)
                        throw new Exception("employer already exist");
                    DataSource_Xml.employerRoot.Add(ConvertEmployer(e));
                    DataSource_Xml.employerRoot.Save(employerPath);
                }
                else throw new Exception("Check your Phone Number again");
            }
            else throw new Exception("Check your Id again");

        }
        void IDAL.removeEmployer(Employer e)
        {
            if (e.CompanyNum > 100000000 && e.CompanyNum < 1000000000)
            {
                if ((e.End).Length == 7)
                {
                    XElement emp = (from item in DataSource_Xml.employerRoot.Elements()
                                    where int.Parse(item.Element("CompanyNum").Value) == e.CompanyNum
                                    select item).FirstOrDefault();
                    if (emp == null)
                        throw new Exception("employer not exist");
                    emp.Remove();
                    DataSource_Xml.employerRoot.Save(employerPath);
                }
                else throw new Exception("Check your Phone Number again");
            }
            else throw new Exception("Check your Id again");
        }
        void IDAL.updateEmployer(Employer e)
        {
            if (e.CompanyNum > 100000000 && e.CompanyNum < 1000000000)
            {
                if ((e.End).Length == 7)
                {
                    XElement emp = (from item in DataSource_Xml.employerRoot.Elements()
                                    where int.Parse(item.Element("CompanyNum").Value) == e.CompanyNum
                                    select item).FirstOrDefault();
                    if (emp == null)
                        throw new Exception("employer not exist");
                    foreach (PropertyInfo item in typeof(Employer).GetProperties())
                        emp.Element(item.Name).SetValue(item.GetValue(e));
                    DataSource_Xml.employerRoot.Save(employerPath);
                }
                else throw new Exception("Check your Phone Number again");
            }
            else throw new Exception("Check your Id again");
        }
        public Employer GetEmployer(int id)
        {
            XElement emp = null;
            try
            {
                emp = (from item in DataSource_Xml.employerRoot.Elements()
                       where int.Parse(item.Element("CompanyNum").Value) == id
                       select item).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
            if (emp == null)
                return null;
            return ConvertEmployer(emp);
        }

        #endregion

        #region specialization functions
        public void addSeciality(Specialization s)
        {
            kodExpert = Convert.ToInt32(DataSource_Xml.configRoot.Element("KodExpert").Value);
            Specialization sp = GetAllSpecialily(x => x.ExpertName == s.ExpertName && x.DomainName == s.DomainName).FirstOrDefault();
            if (sp == null)
            {
                if (s.Expert == 0)
                    s.Expert = kodExpert++;
                else if (s.Expert < 10000000 && s.Expert > 100000000)
                    s.Expert = kodExpert++;
                DataSource_Xml.specializationRoot.Add(new XElement("specialization", new XElement("Expert", s.Expert),
                                                                              new XElement("DomainName", s.DomainName),
                                                                              new XElement("ExpertName", s.ExpertName),
                                                                              new XElement("MinTariff", s.MinTariff),
                                                                              new XElement("MaxTariff", s.MaxTariff)));
                DataSource_Xml.specializationRoot.Save(specializationPath);
            }
            else
            if (s.Expert == 0)
            {
                s.Expert = sp.Expert;
                throw new Exception("specialization num: " + s.Expert);
            }
            else
                throw new Exception("specialization already exist");
            DataSource_Xml.configRoot.Element("KodExpert").Value = kodExpert.ToString();
            DataSource_Xml.configRoot.Save(configPath);

        }
        public void removeSeciality(Specialization s)
        {
            XElement sp = (from item in DataSource_Xml.specializationRoot.Elements()
                           where int.Parse(item.Element("Expert").Value) == s.Expert
                           select item).FirstOrDefault();
            if (sp == null)
                throw new Exception("Specialization not exist");
            sp.Remove();
            DataSource_Xml.specializationRoot.Save(specializationPath);
        }
        void IDAL.updateSeciality(Specialization s)
        {
            Specialization sp = GetSpecialization(s.Expert);
            if (sp == null)
                throw new Exception("specialization not exist");
            removeSeciality(sp);
            addSeciality(s);
        }
        public Specialization GetSpecialization(Domain d, string _name)
        {
            XElement sp = null;
            try
            {
                DataSource_Xml.specializationRoot = XElement.Load(specializationPath);
                sp = (from item in DataSource_Xml.specializationRoot.Elements()
                      where ((Domain)Convert.ToInt32(item.Element("DomainName").Value) == d &&
                      (item.Element("ExpertName").Value) == _name)
                      select item).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
            if (sp == null)
                return null;
            return ConvertSpecialization(sp);
        }
        public Specialization GetSpecialization(int expert)
        {
            XElement sp = null;
            try
            {
                sp = (from item in DataSource_Xml.specializationRoot.Elements()
                      where int.Parse(item.Element("Expert").Value) == expert
                      select item).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
            if (sp == null)
                return null;
            return ConvertSpecialization(sp);
        }

        #endregion

        #region contract functions
        public void addContract(contract c)
        {
            kodContract = Convert.ToInt32(DataSource_Xml.configRoot.Element("KodContract").Value);
            DataSource_Xml.contractRoot = XElement.Load(contractPath);
            XElement con = (from item in DataSource_Xml.contractRoot.Elements()
                            where (int.Parse(item.Element("IdEmployer").Value) == c.IdEmployer) &&
                            (int.Parse(item.Element("IdEmployee").Value) == c.IdEmployee)
                            select item).FirstOrDefault();
            if (con != null)
                throw new Exception("contract already exist");
            c.NumContract = kodContract++;
            DataSource_Xml.contractRoot.Add(new XElement("contract", new XElement("NumContract", c.NumContract),
                                                                             new XElement("IdEmployer", c.IdEmployer),
                                                                             new XElement("IdEmployee", c.IdEmployee),
                                                                             new XElement("Interview", c.Interview),
                                                                             new XElement("Contract", c.Contract),
                                                                             new XElement("BrutoSalary", c.BrutoSalary),
                                                                             new XElement("NetoSalary", c.NetoSalary),
                                                                             new XElement("StartDate", c.StartDate),
                                                                             new XElement("EndDate", c.EndDate),
                                                                             new XElement("MonthHours", c.MonthHours),
                                                                             new XElement("WorkAddress", c.WorkAddress)));
            DataSource_Xml.contractRoot.Save(contractPath);
            DataSource_Xml.configRoot.Element("KodContract").Value = kodContract.ToString();
            DataSource_Xml.configRoot.Save(configPath);
        }
        public void removeContract(contract c)
        {
            XElement con = (from item in DataSource_Xml.contractRoot.Elements()
                            where int.Parse(item.Element("NumContract").Value) == c.NumContract
                            select item).FirstOrDefault();
            if (con == null)
                throw new Exception("contract not exist");
            con.Remove();
            DataSource_Xml.contractRoot.Save(contractPath);
        }
        void IDAL.updateContract(contract c)
        {
            contract con = GetContract_Num(c.NumContract);
            if (con == null)
                throw new Exception("contract not exist");
            removeContract(con);
            addContract(c);
        }
        public contract GetContract_Num(int num)
        {
            XElement con = null;
            try
            {
                con = (from item in DataSource_Xml.contractRoot.Elements()
                       where int.Parse(item.Element("NumContract").Value) == num
                       select item).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
            if (con == null)
                return null;
            return ConvertContract(con);
        }
        public int CountContractsEmployee(contract c)
        {
            return daloosh.CountContractsEmployee(c);
        }
        public int CountContractsEmployer(contract c)
        {
            return daloosh.CountContractsEmployer(c);
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
        #endregion

        #region GetAll
        public IEnumerable<Specialization> GetAllSpecialily(Func<Specialization, bool> predicate = null)
        {
            if (predicate == null)
            {
                return from item in DataSource_Xml.specializationRoot.Elements()
                       select ConvertSpecialization(item);
            }
            return from item in DataSource_Xml.specializationRoot.Elements()
                   let s = ConvertSpecialization(item)
                   where predicate(s)
                   select s;
        }
        public IEnumerable<Employer> GetAllEmployer(Func<Employer, bool> predicate = null)
        {
            if (predicate == null)
            {
                return from item in DataSource_Xml.employerRoot.Elements()
                       select ConvertEmployer(item);
            }
            return from item in DataSource_Xml.employerRoot.Elements()
                   let e = ConvertEmployer(item)
                   where predicate(e)
                   select e;
        }
        public IEnumerable<Employee> GetAllEmployee(Func<Employee, bool> predicate = null)
        {
            if (predicate == null)
            {
                return from item in DataSource_Xml.employeeRoot.Elements()
                       select ConvertEmployee(item);
            }
            else
            {
                return from item in DataSource_Xml.employeeRoot.Elements()
                       let e = ConvertEmployee(item)
                       where predicate(e)
                       select e;
            }
        }
        public IEnumerable<contract> GetAllContract(Func<contract, bool> predicate = null)
        {
            if (predicate == null)
            {
                return from item in DataSource_Xml.contractRoot.Elements()
                       select ConvertContract(item);
            }
            return from item in DataSource_Xml.contractRoot.Elements()
                   let c = ConvertContract(item)
                   where predicate(c)
                   select c;
        }
        #endregion
    }
}
