using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.data
{
    class person
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public int Id { get; set; }
        public int Age { get; set; }
        public int AddressCode { get; set; }
        public int tel1 { get; set; }
        public string label1 { get; set; }
        public int tel2 { get; set; }
        public string label2 { get; set; }

        public person() { }

        public person(string name,string family,int id) {
            this.Name = name;
            this.Family = family;
            this.Id = id;
        }

        public override string ToString()
        {
            return "["+this.Name +" "+ this.Family  +" id="+ this.Id+"]";
        }
    }
}
