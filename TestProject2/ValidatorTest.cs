using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using WpfApp1;

namespace TestProject2
{
    [TestClass]
    public class ValidatorTest
    {

        [DataRow(false,null, null)]
        [DataRow(false,"some text", null)]
        [DataRow(false, null, "sometext")]
        [DataRow(false,"","")]//pattern=inputText=empty
        [DataRow(false,"","some text")]//pattern=empty
        [DataRow(false,"some text","")]//inputText=empty
        [DataRow(false,"some pattern","select")]
        [DataRow(false,"some pattern","where")]
        [DataRow(false,"some pattern","join")]
        [DataRow(false,"some pattern","insert")]
        [DataRow(false,"some pattern","like")]
        [DataRow(true, @"^09\d{2}\d{3}\d{2}\d{2}$", "09012345678")]
        [DataRow(false, @"^09\d{2}\d{3}\d{2}\d{2}$", "0901234567")]
        [DataRow(false, @"^09\d{2}\d{3}\d{2}\d{2}$", "090123456789")]
        [DataRow(false, @"^09\d{2}\d{3}\d{2}\d{2}$", "19012345678")]
        [DataRow(true, @"^[A-Za-z\d\s]*$", " ")]
        [DataRow(true, @"^[A-Za-z\d\s]*$", "A")]
        [DataRow(true, @"^[A-Za-z\d\s]*$", "z")]
        [DataRow(true, @"^[A-Za-z\d\s]*$", "z A")]
        [DataRow(true, @"^[A-Za-z\d\s]*$", "z A 1")]
        [DataRow(false, @"^[A-Za-z\d]*$", "z A 1")]
        [DataRow(false, @"^[A-Za-z]*$", "z1")]
        [TestMethod]
        public void TestIsMatch(bool expected,string pattern,string input)
        {
            //Arrang

            //Fact
            bool result=WpfApp1.Validation.Validator.IsMatch(pattern, input);
            //Assert
            Assert.AreEqual(expected,result);
        }



        [DataRow(false,null)]
        [DataRow(false,"")]
        [DataRow(true,"22088888888")]
        [DataRow(true, "022088888888")]
        [DataRow(false, "220888888889")]
        [DataRow(false, "922088888888")]
        [DataRow(false, "2088888888")]
        [DataRow(false, "088888888")]
        [TestMethod]
        public void TestEshterakValidation(bool expected,string input)
        {
            //Arrang
            WpfApp1.Validation.Validator validator=new WpfApp1.Validation.Validator();

            //Fact
            bool result = validator.EshterakValidtaion(input);

            //Assert
            Assert.AreEqual(expected,result);
        }


        [DataRow(false,null)]
        [DataRow(false,"")]
        [DataRow(false,"some text")]
        [DataRow(false,"02")]
        [DataRow(false,"02.02")]
        [DataRow(false,"02.02.")]
        [DataRow(false,"020")]
        [DataRow(false,"0200")]
        [DataRow(false,"02.200")]
        [DataRow(false,".02.200")]
        [DataRow(false,".02.2f")]
        [DataRow(true,"02.05.0125.0001")]
        [DataRow(true,"02.05.0125")]
        [TestMethod]
        public void TestAddressCodeValidation(bool expected,string input)
        {
            //Arrang
            WpfApp1.Validation.Validator validator = new WpfApp1.Validation.Validator();

            //Fact
            bool result=validator.AddressCodeValidation(input);

            //Assert
            Assert.AreEqual(expected,result);
        }


        [DataRow(true, null)]
        [DataRow(true, "")]
        [DataRow(true, "09145444261")]
        [DataRow(true, "09000000000")]
        [DataRow(true, "09999999999")]
        [DataRow(false, "0999999999")]
        [DataRow(false, "09")]
        [DataRow(false, "099999999999")]
        [DataRow(false, "099999999999999")]
        [DataRow(false, "0999999999f")]
        [DataRow(false, "some text")]
        [TestMethod]
        public void TestPhoneValidation(bool expected,string input)
        {
            //Arrang
            WpfApp1.Validation.Validator validator= new WpfApp1.Validation.Validator();

            //Fact
            bool result=validator.phoneValidation(input);

            //Assert
            Assert.AreEqual(expected,result);
        }


        [DataRow(true, null)]
        [DataRow(true, "")]
        [DataRow(true, " ")]
        [DataRow(true, "some text")]
        [DataRow(false, "digit 12345")]
        [DataRow(false, "special +:{}[]")]
        [TestMethod]
        public void TestNameFamilyValidation(bool expected,string input)
        {
            //Arrang
            WpfApp1.Validation.Validator validator = new WpfApp1.Validation.Validator();

            //Fact
            bool result = validator.NameFamilyValidation(input);

            //Assert
            Assert.AreEqual(expected,result);
        }

        [DataRow(true, null)]
        [DataRow(true,"")]
        [DataRow(true," ")]
        [DataRow(true,"some text")]
        [DataRow(true,"digit 12345")]
        [DataRow(false,"special +-*[]{}#$()")]
        [TestMethod]
        public void TestTozihat(bool expected, string input)
        {
            //Arrang
            WpfApp1.Validation.Validator validator = new WpfApp1.Validation.Validator();

            //Fact
            bool result = validator.TozihatValidation(input);

            //Assert
            Assert.AreEqual(expected,result);
        }
    }
}