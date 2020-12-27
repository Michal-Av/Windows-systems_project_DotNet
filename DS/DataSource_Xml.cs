using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
    public class DataSource_Xml
    {
        public static XElement employeeRoot = new XElement("EMPLOYEES");
        public static XElement employerRoot = new XElement("EMPLOYERS");
        public static XElement specializationRoot = new XElement("SPECIALIZATIONS");
        public static XElement contractRoot = new XElement("CONTRACTS");
        public static XElement configRoot;
    }
}
