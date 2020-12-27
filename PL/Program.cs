//Talia Tsameret 207045956 Michal Avraham 313206419
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;
namespace PL
{
    class Program
    {
        static BL.IBL bl = BL.FactoryBl.GetBL();
        static void Main(string[] args)
        {
            try
            {
                //bl.addEmployee(new Employee
                //{
                //    Id = 12345678,
                //    LastName = "Avraham",
                //    FirstName = "Michal",
                //    Army = false,
                //    Education = education.BA,
                //    City = "Zefat",
                //    //CheckExpert = 0,
                //    Date = new DateTime(day: 16, month: 11, year: 1995),
                //    Address = "Yafo 2",
                //    PhoneNumber = "0508265431",
                //    // BankAccount = new bankAccount(134, "discont", 5432, new branch(123, "Najara 26", "Jerusalem")),
                //    Special = new Specialization
                //    {
                //        DomainName = Domain.security,
                //        Expert = 0,
                //        ExpertName = "Java_security"
                //    }
                //});
                //bl.addEmployee(new Employee
                //{
                //    Id = 82436543,
                //    LastName = "Cohen",
                //    FirstName = "Sason",
                //    Army = true,
                //    City = "Jerusalem",
                //    Education = education.Student,
                //    CheckExpert = 98765432,
                //    Date = new DateTime(day: 23, month: 4, year: 1993),
                //    //BankAccount = new bankAccount(233, "Leumi", 6542, new branch(818, "Jerusalem 37", "Zefat")),
                //    //Special = new Specialization
                //    // {
                //    //     DomainName = Domain.Databases,
                //    //     Expert = 0,
                //    //     ExpertName = "ima"
                //    // }
                //});

                bl.addEmployer(new Employer
                {
                    CompanyNum = 98643322,
                    LastName = "Tsameret",
                    FirstName = "Talia",
                    establish = new DateTime(day: 10, month: 1, year: 1980),
                    CompanyName = "Elta",
                    DomainName = Domain.security,
                    EmployerStatus = false,
                    City = "Ashdod"
                });
                bl.addEmployer(new Employer
                {
                    CompanyNum = 19808621,
                    LastName = "Levi",
                    FirstName = "David",
                    establish = new DateTime(day: 3, month: 1, year: 2015),
                    CompanyName = "Davidiyan",
                    DomainName = Domain.Databases,
                    EmployerStatus = true,
                    City = "Beer sheva"
                });
                bl.addContract(new contract
                {
                    IdEmployee = 12345678,
                    IdEmployer = 98643322,
                    Contract = true,
                    StartDate = new DateTime(day: 23, month: 4, year: 2010),
                    EndDate = new DateTime(day: 23, month: 1, year: 2017),
                    BrutoSalary = 100,
                    NetoSalary = 90,
                    MonthHours = 160,
                    Interview = true
                });
                bl.addContract(new contract
                {
                    IdEmployee = 82436543,
                    IdEmployer = 98643322,
                    Contract = true,
                    StartDate = new DateTime(day: 23, month: 4, year: 2010),
                    EndDate = new DateTime(day: 23, month: 1, year: 2017),
                    BrutoSalary = 100,
                    NetoSalary = 90,
                    MonthHours = 160,
                    Interview = true
                });
                bl.addContract(new contract
                {
                    IdEmployee = 12345678,
                    IdEmployer = 19808621,
                    Contract = true,
                    StartDate = new DateTime(day: 23, month: 4, year: 2017),
                    EndDate = new DateTime(day: 23, month: 1, year: 2018),
                    BrutoSalary = 100,
                    NetoSalary = 90,
                    MonthHours = 160,
                    Interview = true
                });
                bl.addSeciality(new Specialization
                {
                    DomainName = Domain.communications,
                    Expert = 0,
                    ExpertName = "Java_security",
                });
                bl.updateContract(new contract
                {
                    IdEmployee = 82436543,
                    IdEmployer = 98643322,
                    Contract = true,
                    StartDate = new DateTime(day: 23, month: 4, year: 2010),
                    EndDate = new DateTime(day: 23, month: 2, year: 2017),
                    BrutoSalary = 100,
                    NetoSalary = 90,
                    MonthHours = 100,
                    Interview = true,
                    NumContract = 10000001
                });
                //bl.updateEmployee(new Employee
                //{
                //    Id = 82436543,
                //    LastName = "Cohen",
                //    FirstName = "Sason",
                //    Army = true,
                //    City = "Ashdod",
                //    Education = education.Student,
                //    //CheckExpert = 98765432,
                //    Special = new Specialization
                //    {
                //        DomainName = Domain.communications,
                //        Expert = 0,
                //        ExpertName = "Java_security",

                //    },
                //    Date = new DateTime(day: 23, month: 4, year: 1993),
                //    // BankAccount = new bankAccount(233, "Leumi", 6542, new branch(818, "Jerusalem 37", "Zefat"))
                //});
                bl.removeEmployer(bl.GetEmployer(19808621));

                Console.WriteLine("actions:");
                Console.WriteLine("0: to exit");
                Console.WriteLine("1: print all specialization");
                Console.WriteLine("2: print all employees");
                Console.WriteLine("3: print all employers");
                Console.WriteLine("4: Number of Private Business");
                Console.WriteLine("5: all old contract");
                Console.WriteLine("6: all employees after army");
                Console.WriteLine("7: all employees who live far from work");
                Console.WriteLine("8: Group Contract By Specialization");
                Console.WriteLine("9: Group Contract By City");
                Console.WriteLine("10: all employees ordered by last name");
                Console.WriteLine("11: Get All Contract Students that end next month:");
                int choice = int.Parse(Console.ReadLine());
                do
                {
                    try
                    {
                        switch (choice)
                        {
                            case 0: Console.Write("Exit:"); break;
                            case 1:
                                Console.Write("all specialization:");
                                foreach (var item in bl.GetAllSpecialily())
                                    Console.WriteLine(item);
                                Console.WriteLine();
                                break;
                            case 2:
                                Console.Write("all employees:");
                                foreach (var item in bl.GetAllEmployee())
                                    Console.WriteLine(item);
                                Console.WriteLine();
                                break;
                            case 3:
                                Console.Write("all employers:");
                                foreach (var item in bl.GetAllEmployer())
                                    Console.WriteLine(item);
                                Console.WriteLine();
                                break;
                            case 4:
                                Console.Write("Number of Private Business:");
                                Console.WriteLine(bl.GetNumPrivateBusiness() + "\n");
                                break;
                            case 5:
                                Console.Write("all old contract:");
                                foreach (var item in bl.GetAllOldContract())
                                    Console.WriteLine(item);
                                Console.WriteLine();
                                break;
                            case 6:
                                Console.Write("all employees after army:");
                                foreach (var item in bl.GetAllEmployee(i => i.Army))
                                    Console.WriteLine(item);
                                Console.WriteLine();
                                break;
                            case 7:
                                Console.WriteLine("all employees who live far from work");
                                foreach (var item in bl.GetAllFarEmployee())
                                    Console.WriteLine(item);
                                Console.WriteLine();
                                break;
                            case 8:
                                Console.WriteLine("Group Contract By Specialization");
                                foreach (IGrouping<int, contract> group in bl.GroupContractBySpecialization(true))
                                {
                                    Console.WriteLine("\nSpecialization {0}", group.Key);
                                    foreach (contract item in group)
                                        Console.WriteLine("{0}", item);
                                }
                                break;
                            case 9:
                                Console.WriteLine("Group Contract By City");
                                foreach (IGrouping<string, contract> group in bl.GroupContractByCity(true))
                                {
                                    Console.WriteLine("\nCity {0}", group.Key);
                                    foreach (contract item in group)
                                        Console.WriteLine("{0}", item);
                                }
                                Console.WriteLine();
                                break;
                            case 10:
                                Console.Write("all employees ordered by last name:");
                                foreach (var item in bl.GetAllEmployeeLastName())
                                    Console.WriteLine(item);
                                Console.WriteLine();
                                break;
                            case 11:
                                Console.WriteLine("Get All End Contract Student:");
                                foreach (var item in bl.GetAllEndContractStudent())
                                    Console.WriteLine(item);
                                Console.WriteLine();
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    choice = int.Parse(Console.ReadLine());
                } while (choice != 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return;
        }

    }
}