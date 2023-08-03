using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace WpfApp1.Validation
{
   public class Validator
    {
        private static List<string> keyWord = new List<string>() { "select", "where", "from","copy", "update", "insert", "delete", "join", "exist", "like" };

        private const string eshterakPattern = @"^0?22[0|1]\d{8}$";
        private const string eshterakPattern_digit = @"^\d*$";
        private const string addressCodePattern = @"^\d{2}[.]\d{2}[.]\d{4}([.]\d{4})?$";
        private const string addressCodePattern_digit = @"^\d*[.]*$";
        private const string phonePattern = @"^09\d{2}\d{3}\d{2}\d{2}$";
        private const string phonePattern_digit = @"^\d*$";
        private const string tozihatPattern = @"^[A-Za-z\d\s]*$";
        private const string nameFamilyPattern = @"^[A-Za-z\s]*$";

        public string EshterakPattern { get { return eshterakPattern; } set { } }
        public string EshterakPattern_digit { get { return eshterakPattern_digit; } set { } }
        public string AddressCodePattern { get { return addressCodePattern; } set { } }
        public string AddressCodePattern_digit { get {return addressCodePattern_digit; } set { } }
        public string PhonePattern { get { return phonePattern; } set { } }
        public string PhonePattern_digit { get { return phonePattern_digit; } set { } }
        public string TozihatPattern { get { return tozihatPattern; } set { } }
        public string NameFamilyPattern { get { return nameFamilyPattern; } set { } }

        /// <summary>
        /// return true if text is in correct format(pattern)
        /// </summary>
        /// <param name="pattern">string format</param>
        /// <param name="input">input text</param>
        /// <returns>boolean</returns>
        public static bool IsMatch(string pattern, string input) {
            if (string.IsNullOrEmpty(pattern)|| string.IsNullOrEmpty(input))
            {
                return false;
            }

            foreach (var item in keyWord)
            {
                if (input.ToLower().Contains(item))
                {
                    return false;
                }
            }

            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// validating input with eshterak pattern
        /// </summary>
        /// <param name="input">eshterak</param>
        /// <returns>boolean</returns>
        public bool EshterakValidtaion(string input) {
            //if (string.IsNullOrEmpty(input)) { return false; }
            //Regex regex = new Regex(EshterakPattern);
            //return regex.IsMatch(input);
            return IsMatch(EshterakPattern, input);
        }
        //public bool Eshterak_inputValidation(string input) {
        //    Regex regex = new Regex(EshterakPattern_digit);
        //    return regex.IsMatch(input);
        //}

        /// <summary>
        /// validating input with addresscode pattern
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>boolean</returns>
        public bool AddressCodeValidation(string input) {
            //if (string.IsNullOrEmpty(input)) { return false; }
            //return new Regex(AddressCodePattern).IsMatch(input);
            return IsMatch(AddressCodePattern, input);
        }
        //public bool addressCode_inputValidation(string input) {

        //    return new Regex(AddressCodePattern_digit).IsMatch(input);
        //}

        /// <summary>
        /// validating input with phone pattern
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool phoneValidation(string input) {
            if (string.IsNullOrEmpty(input))
            {
                return true;
            }
            return IsMatch(phonePattern, input);// new Regex(PhonePattern).IsMatch(input);
        }
        /// <summary>
        /// validating input with NameFamily pattern
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>boolean</returns>
        public bool NameFamilyValidation(string input) {
            if (string.IsNullOrEmpty(input))
            {
                return true;
            }
            return IsMatch(NameFamilyPattern, input);
        }

        /// <summary>
        /// validating input with Tozihat pattern
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>boolean</returns>
        public bool TozihatValidation(string input) {
            if (string.IsNullOrEmpty(input))
            {
                return true;
            }
            return IsMatch(TozihatPattern, input);
        }
    }
}
