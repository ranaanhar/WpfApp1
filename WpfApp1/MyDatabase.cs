using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WpfApp1
{
    internal class MyDatabase
    {
        //private string connectionString = "server=localhost;user id=root;persistsecurityinfo=True;database=gdb";
        private MySqlConnection sqlConnection;
        private string server, password, database, uid;
        private string message = "";

        public MyDatabase()
        {
            //initialize();
            string connectionString = "server=localhost;UID=root;PASSWORD=azera5090268866code;database=gdb";
            sqlConnection = new MySqlConnection(connectionString);
        }

        private void initialize(string server, string uid, string password, string database)
        {
            this.server = server;
            this.password = password;
            this.uid = uid;
            this.database = database;
        }
        public int ExecuteProcedure(MySqlCommand sqlCommand) {
            int result = -1;
            try
            {
                if (OpenConnection()==true)
                {
                    sqlCommand.Connection= sqlConnection;
                    result=sqlCommand.ExecuteNonQuery();
                }
            }
            catch (MySqlException)
            {

            }
            catch (Exception)
            {

            }
            finally {
                CloseConnection();
            }
            return result;
        }

        /// <summary>
        /// return result of select
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <returns></returns>
        public DataTable ExecuteProcedureReader(MySqlCommand sqlCommand)
        {
            DataTable table = null;
            try
            {
                if (OpenConnection() == true)
                {
                    sqlCommand.Connection = sqlConnection;
                    MySqlDataReader reader = sqlCommand.ExecuteReader();
                    table = new System.Data.DataTable();
                    table.Load(reader);
                    reader.Close();
                }
            }
            catch (MySqlException)
            {

            }
            catch (Exception)
            {

            }
            finally
            {
                CloseConnection();
            }
            return table;
        }

        public int ExecuteQuery(string query) {
            int arg = 0;
            try
            {
                if (OpenConnection() == true)
                {
                    MySqlCommand command = new MySqlCommand(query, this.sqlConnection);
                    arg = command.ExecuteNonQuery();
                    
                }
            }
            catch (MySqlException exp)
            {
                this.message = exp.Message;

            }
            catch (Exception exp)
            {
                this.message += exp.Message;

            }
            finally {
                this.CloseConnection();
            }
            return arg;
        }

        public System.Data.DataTable ExecuteReader(string query) {
            System.Data.DataTable table = null;
            try
            {
                if (OpenConnection() == true)
                {
                    MySqlCommand command = new MySqlCommand(query, this.sqlConnection);
                    MySqlDataReader reader = command.ExecuteReader();
                    table = new System.Data.DataTable();
                    table.Load(reader);
                    reader.Close();
                }
            }
            catch (MySqlException exp)
            {
                this.message = exp.Message;
            }
            catch (Exception exp) {
                this.message += exp.Message;
            }
            finally 
            { 
                this.CloseConnection(); 
            }
            return table;
        }
        /*
        public string createTable()
        {
            try
            {
                string query = "CREATE TABLE if not exists person (Name VARCHAR(50)," +
                "Family VARCHAR(50),Age INT,Id INT)";
                if (OpenConnection() == true)
                {
                    int arg = 0;
                    MySqlCommand command = new MySqlCommand(query, sqlConnection);
                    arg = command.ExecuteNonQuery();
                    this.CloseConnection();
                    message = arg + " row affected.";
                }
            }
            catch (MySqlException exp)
            {
                message = exp.Message;
            }
            return message;
        }

        public List<data.person> Select()
        {

            string query = "SELECT * FROM person";
            List<WpfApp1.data.person> personList = new List<data.person>();
            try
            {
                if (OpenConnection() == true)
                {
                    MySqlCommand command = new MySqlCommand(query, sqlConnection);
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        try
                        {
                            data.person p = new data.person();

                            p.Name = reader["Name"].ToString();
                            p.Family = reader["Family"].ToString();
                            //p.Age = Convert.ToInt32(reader["Age"]);
                            p.Id = Convert.ToInt32(reader["Id"]);

                            personList.Add(p);
                        }
                        catch (Exception exp)
                        {
                            message = exp.Message;
                        }
                    }

                    reader.Close();
                    CloseConnection();
                }
            }
            catch (MySqlException exp)
            {
                message = exp.Message;
            }
            return personList;
        }

        public string Insert(data.person p)
        {
            try
            {
                string values = " VALUES(\'" + p.Name + "\',\'" + p.Family + "\',\'" + p.Id + "\')";
                string query = "INSERT INTO person (Name,Family,Age,Id)" + values;
                if (OpenConnection() == true)
                {
                    int arg = -1;
                    MySqlCommand command = new MySqlCommand(query, sqlConnection);
                    arg = command.ExecuteNonQuery();
                    message = arg + " row affected.";
                    this.CloseConnection();
                }
            }
            catch (MySqlException exp)
            {
                message = exp.Message;
            }
            return message;
        }

        public string Update(data.person p)
        {
            string query = "UPDATE person SET Name='"+p.Name+"',Family='"+p.Family+"' WHERE Id='"+p.Id+"'";
            try
            {
                if (OpenConnection() == true)
                {
                    int arg = -1;
                    MySqlCommand command = new MySqlCommand(query, sqlConnection);
                    arg=command.ExecuteNonQuery();
                    message = arg + " rows affected";
                    this.CloseConnection();
                }
            }
            catch (MySqlException exp)
            {
                message = exp.Message;
            }
            return message;
        }

        public string Delete(data.person p)
        {
            string query = "DELETE FROM person WHERE id=\'"+p.Id+"\'";
            try
            {
                if (OpenConnection() == true)
                {
                    int arg = -1;
                    MySqlCommand command = new MySqlCommand(query, sqlConnection);
                    arg=command.ExecuteNonQuery();
                    message = arg + " rows affected";
                    this.CloseConnection();
                }
            }
            catch (MySqlException exp)
            {
                message = exp.Message;
            }
            return message;
        }
        */
        public int Count(MySqlCommand sqlCommand) {
            int count = -1;
            //string query = "SELECT COUNT(*) FROM person";
            try
            {
                if (OpenConnection() == true)
                {
                    sqlCommand.Connection= this.sqlConnection;
                    count = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    CloseConnection();
                }
            }
            catch (MySqlException exp)
            {
                message = exp.Message;
            }
            return count;
        }

        //public string testConnection()
        //{
        //    string msg;//= "preparing...";
        //    if (OpenConnection())
        //    {
        //        msg = "Connection Extablished!";
        //        if (CloseConnection())
        //        {
        //            msg += "\nConnection Closed Successfully!";
        //        }
        //        else
        //        {
        //            msg += "\nCan not Close Connection!\n" + this.message;
        //        }
        //    }
        //    else
        //    {
        //        msg = "Can not Open Connection!\n" + this.message;
        //    }
        //    return msg;
        //}


        /// <summary>
        /// open connection
        /// </summary>
        /// <returns>boolean</returns>
        private bool OpenConnection()
        {
            try
            {
                sqlConnection.Open();
                return true;
            }
            catch (MySqlException exp)
            {

                switch (exp.Number)
                {
                    case 0:
                        message = "Can not connect to mysql server. Contact your administrator.";
                        break;
                    case 1045:
                        message = "Invalid username or password,please try again";
                        break;
                    default:
                        message = exp.Message;
                        break;
                }

                return false;
            }
        }

        /// <summary>
        /// close sql connection
        /// </summary>
        /// <returns>boolean</returns>
        private bool CloseConnection()
        {
            
            try
            {
                sqlConnection.Close();
                return true;
            }
            catch (MySqlException exp)
            {
                message = exp.Message;
                return false;
            }
        }

        public bool TestConnection() {
            if (OpenConnection())
            {
                if (CloseConnection())
                {
                    return true;
                }
            }
            return false;
        }

    }
}
