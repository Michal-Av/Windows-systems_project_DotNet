using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class contract
    {
        public static int KodContract = 10000000;
        public int NumContract { get; set; }
        public int IdEmployer { get; set; }
        public int IdEmployee { get; set; }
        public bool Interview { get; set; }
        public bool Contract { get; set; }
        public double BrutoSalary { get; set; }
        public double NetoSalary { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MonthHours { get; set; }
        public string WorkAddress { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
