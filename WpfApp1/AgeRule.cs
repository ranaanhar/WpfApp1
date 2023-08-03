using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp1
{
    class AgeRule:ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }
        //public AgeRule() { }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string str = value as string;
            int age = 0;
            try
            {
                if (str.Length>0)
                {
                    age = Int32.Parse(str);
                }
            }
            catch (Exception exp)
            {
                return new ValidationResult(false, $"illegal character or {exp.Message}");
            }
            if (age>Max||age<Min)
            {
                return new ValidationResult(false, $"please enter an age in range {Min} and {Max}");
            }
            return ValidationResult.ValidResult;
        }
    }
}
