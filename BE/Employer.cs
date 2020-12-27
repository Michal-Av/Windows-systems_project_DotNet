using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Employer
    {
        public int CompanyNum { get; set; }
        public bool EmployerStatus { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string CompanyName { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string PhoneNumber { get; set; }
        //{
        //    get { return Start + End; }
        //    set { PhoneNumber = value; }
        //}
        public string Address { get; set; }
        public string City { get; set; }
        public Domain DomainName { get; set; }
        public DateTime establish { get; set; }
        public string ImageSource { get; set; }
        public Employer()
        {
            ImageSource = (@"default.png");

        }
        public override string ToString()
        {
            return this.ToStringProperty();
        }

    }
}
