using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    class Moshtarak
    {
        public string Name { get; set; }
        public string Family { get; set; }

        public string Tel1 { get; set; }
        public string Label1 { get; set; }
        public string Tel2 { get; set; }
        public string Label2 { get; set; }

        public string Eshterak { get; set; }
        public string AddressCode { get; set; }

        public override string ToString()
        {
            return this.Name+this.Family+this.Eshterak + this.AddressCode+this.Tel1+this.Label1+this.Tel2+this.Label2;
        }
    }
}
