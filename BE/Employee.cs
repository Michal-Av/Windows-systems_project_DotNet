using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{

    public struct ATM
    {
        public int BankCode { get; set; }
        public string BankName { get; set; }
        public int ATMCode { get; set; }
        public string ATMAddress { get; set; }
        public string City { get; set; }
        public ATM(string _bankName, int _bankNum, string _city, int _atmCode, string _atmAddress)
        {
            BankCode = _bankNum;
            BankName = _bankName;
            ATMCode = _atmCode;
            ATMAddress = _atmAddress;
            City = _city;
        }
        public override string ToString()
        {
            return this.ToStringProperty();
        }

    }


    public class Employee
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime Date { get; set; }
        public string PhoneNumber { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public education Education { get; set; }
        public bool Army { get; set; }
        public string NumSpecialization{ get; set; }
        public string ImageSource { get; set; }
        
        public Employee()
        {
            ImageSource = (@"default.png");
        }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
