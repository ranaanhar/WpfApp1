using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using WpfApp1;
using WpfApp1.DatabaseHandler;

namespace TestProject2
{
    [TestClass]
    public class MyDatabase
    {
        [TestMethod]
        public void TestConnection()
        {
            //Arrange
            WpfApp1.MyDatabase myDatabase = new WpfApp1.MyDatabase();
           
            //Fact
            bool result=myDatabase.TestConnection();

            //Assert
            Assert.IsTrue(result);
        }
    }

    [TestClass]
    public class MoshtarakDbHandler
    {
        WpfApp1.DatabaseHandler.MoshtarakDbHandler moshtarakDbHandler;
        string tableName = "gdb.test1", col1 = "name", col2 = "family";

        public MoshtarakDbHandler() {
            moshtarakDbHandler =
                    new WpfApp1.DatabaseHandler.MoshtarakDbHandler(
                        new WpfApp1.MyDatabase());
        }

        [TestInitialize]
        public void TestInitialize()
        {

            string query = string.Format("CREATE TABLE IF NOT EXISTS {0} ({1} INTEGER,{2} VARCHAR(50));", tableName, col1, col2);
            //string query2 = string.Format("INSERT INTO {0} ({1},{2}) VALUES ({3},{4});",tableName,col1,col2,"testValue1","testValue2");
             moshtarakDbHandler.CreateTable(query);

            
        }

        [TestMethod]
        public void TestCreateTable()
        {
            
            

                ////Arrange           
                //string query = string.Format("CREATE TABLE IF NOT EXISTS {0} ({1} VARCHAR(50),{2} VARCHAR(50));", "gdb.test", "col1", "col2");

                ////Fact
                //int result = moshtarakDbHandler.CreateTable(query);

                ////Assert
                //Assert.AreNotEqual(result, -1);
            
        }

        [TestMethod]
        public void MyInsertMethod()
        {
            string query =string.Format( "INSERT INTO {0} ({1},{2}) VALUES ({3},{4});",tableName,col1,col2,"testValue1","testValue2");
            WpfApp1.MyDatabase myDatabase=new WpfApp1.MyDatabase();
            
            int result=myDatabase.ExecuteQuery(query);

            Assert.AreEqual(result,0);
            
        }


        [TestCleanup]
        public void TestCleanup()
        {
            string query = string.Format("DROP TABLE {0};", tableName);
            moshtarakDbHandler.CreateTable(query);


        }
    }
}
