using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Specialization
    {
        public static int KodExpert = 10000000;
        public int Expert { get; set; }
        public Domain DomainName { get; set; }
        public string ExpertName { get; set; }
        public double MinTariff { get; set; }
        public double MaxTariff { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
