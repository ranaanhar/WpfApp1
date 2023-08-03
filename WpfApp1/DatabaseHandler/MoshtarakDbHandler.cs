using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WpfApp1.Model;

namespace WpfApp1.DatabaseHandler
{
    internal class MoshtarakDbHandler
    {
        private readonly string
            InsertMoshtarakProcedure = "insert_moshtarak_procedure",
            DeleteMoshtarakProcedure = "delete_moshtarak_procedure",
            UpdateMoshtarakProcedure = "update_moshtarak_procedure",
            SelectMoshtarakProcedure = "select_moshtarak_procedure",
            CountMoshtarakProcedure = "count_moshtarak_procedure";
        private readonly string P_Eshterak = "@_eshterak", P_Addresscode = "@_addresscode", P_Name = "@_name", P_Family = "@_family",
            P_Tel1 = "@_tel1", P_Tel2 = "@_tel2", P_Lable1 = "@_label1", P_Lable2 = "@_label2", P_Limit = "@_limit", P_Offset = "@_offset";
        //private string limit = "100", offset = "0";
        private const string _Name = "Name", _Family = "Family", _Tel1 = "Tel1", _Tel2 = "Tel2",
            _Label1 = "Label1", _Label2 = "Label2", _Eshterak = "Eshterak", _AddressCode = "AddressCode", _TableName = "gdb.Moshtarak";
        //private readonly string createTableQuery = "CREATE TABLE if not exists {0} ({1} VARCHAR(50),{2} VARCHAR(50),{3} VARCHAR(50)," +
        //        "{4} VARCHAR(50),{5} VARCHAR(50),{6} VARCHAR(50),{7} VARCHAR(50),{8} VARCHAR(50))",
        //    insertQuery = "INSERT INTO {0} ({1},{2},{3},{4},{5},{6},{7},{15}) VALUES ('{8}',{9},{10},'{11}','{12}','{13}','{14}','{16}')",
        //    updateQuery = "UPDATE {0} SET {1}={8},{2}='{9}',{3}='{10}',{4}='{11}',{5}='{12}',{6}='{13}',{7}='{14}' WHERE {15}={16}",
        //    deleteQuery = "DELETE FROM {0} WHERE {1}={2}",
        //    countQuery = "SELECT COUNT(*) FROM {0}  WHERE {1} LIKE '%{2}%' AND {3} LIKE '%{4}%' AND {5} LIKE '%{6}%' " +
        //    "AND {7} LIKE '%{8}%' AND {9} LIKE '%{10}%' AND {11} LIKE '%{12}%' AND {13} LIKE '%{14}%' AND {15} LIKE '%{16}%'",
        //    //selectQuery = "SELECT * FROM {0} ORDER BY {1} ASC LIMIT {2} OFFSET {3}",
        //    searchQuery = "SELECT * FROM {0} WHERE {4} LIKE '%{5}%' AND {6} LIKE '%{7}%' AND {8} LIKE '%{9}%' AND {10} LIKE '%{11}%' AND {12} LIKE '%{13}%' AND {14} LIKE '%{15}%' AND {16} LIKE '%{17}%' AND {18} LIKE '%{19}%' " +
        //    "ORDER BY {1} ASC LIMIT {2} OFFSET {3}";
        private string message = "";
        private MyDatabase database;
        public MoshtarakDbHandler(MyDatabase database)
        {
            this.database = database;
        }

        public int CreateTable(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return -1;
            }
            int resultcode = -1;
            try
            {
                //string query = string.Format(createTableQuery, _TableName, _Name, _Eshterak, _AddressCode, _Tel1, _Tel2, _Label1, _Label2, _Family);
                resultcode = this.database.ExecuteQuery(query);
            }
            catch (Exception exp)
            {
                //logger.log()
                this.message = exp.Message;
            }
            return resultcode;
        }

        /// <summary>
        /// search database for moshtarak
        /// </summary>
        /// <param name="moshtarak"></param>
        /// <param name="limit">limit number of result</param>
        /// <param name="offset">offset of limit based search</param>
        /// <param name="count">number of search result</param>
        /// <returns></returns>
        public List<Model.Moshtarak> Search(Moshtarak moshtarak, int limit, int offset, out int count)
        {
            count = Count(moshtarak);

            MySqlCommand sqlCommand = new MySqlCommand(SelectMoshtarakProcedure);
            SetSqlCommandParameter(sqlCommand, moshtarak, limit, offset);
            return GetMoshtarakFromSqlCommand(sqlCommand);
        }


        /// <summary>
        /// get list of moshtarak from sqlCommand
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <returns></returns>
        List<Model.Moshtarak> GetMoshtarakFromSqlCommand(MySqlCommand sqlCommand)
        {

            System.Data.DataTable table = this.database.ExecuteProcedureReader(sqlCommand);
            List<Model.Moshtarak> result = new List<Model.Moshtarak>();
            try
            {
                if (table != null)
                    foreach (System.Data.DataRow row in table.Rows)
                    {
                        string eshterak = row[_Eshterak].ToString();
                        //if (!string.IsNullOrEmpty(eshterak))

                        result.Add(new Model.Moshtarak
                        {
                            Eshterak = eshterak,
                            Name = row[_Name].ToString(),
                            Family = row[_Family].ToString(),
                            AddressCode = row[_AddressCode].ToString(),
                            Tel1 = row[_Tel1].ToString(),
                            Tel2 = row[_Tel2].ToString(),
                            Label1 = row[_Label1].ToString(),
                            Label2 = row[_Label2].ToString()
                        });

                    }
            }
            catch (Exception exp)
            {
                this.message += exp.Message;
            }
            return result;
        }



        /// <summary>
        /// insert moshtarak into database
        /// </summary>
        /// <param name="moshtarak"></param>
        /// <returns></returns>
        public int Insert(Model.Moshtarak moshtarak)
        {
            int result = -1;
            //this.message += "in insert mothod.";
            try
            {
                string addresscode = moshtarak.AddressCode;
                if (moshtarak == null)
                {
                    this.message = "moshtarak ra sahih vared konid.";
                    return result;
                }
                if (moshtarak.AddressCode != "NULL")
                {
                    addresscode = "'" + moshtarak.AddressCode + "'";
                }
                else
                {
                    if (moshtarak.Eshterak == "NULL")
                        return result;
                }

                MySqlCommand sqlCommand = new MySqlCommand(InsertMoshtarakProcedure);
                SetSqlCommandParameter(sqlCommand, moshtarak);

                result = database.ExecuteProcedure(sqlCommand);

                //string query = string.Format(this.insertQuery, _TableName, _Name, _Eshterak, _AddressCode, _Tel1, _Tel2, _Label1, _Label2,
                //    moshtarak.Name, moshtarak.Eshterak, addresscode, moshtarak.Tel1, moshtarak.Tel2, moshtarak.Label1, moshtarak.Label2, _Family, moshtarak.Family);
                //arg = this.database.ExecuteQuery(query);

            }
            catch (Exception exp)
            {
                this.message += "\nexp" + exp.Message;
            }
            return result;
        }



        /// <summary>
        /// update moshtarak in database
        /// </summary>
        /// <param name="moshtarak"></param>
        /// <returns></returns>
        public int Update(Model.Moshtarak moshtarak)
        {
            int result = -2;
            //string addresscode = moshtarak.AddressCode, eshterak = moshtarak.Eshterak;
            //string query;//= "";
            try
            {

                if (moshtarak == null)
                {
                    this.message = "moshtarak ra sahih vared konid.";
                    return result;
                }
                //if (moshtarak.AddressCode != "NULL")
                //{
                //    addresscode = "'" + moshtarak.AddressCode + "'";
                //}

                //if (moshtarak.Eshterak != "NULL")
                //{
                //    eshterak = "'" + moshtarak.Eshterak + "'";
                //}

                //query = string.Format(this.updateQuery, _TableName, _AddressCode, _Name, _Family, _Tel1, _Tel2, _Label1, _Label2,
                //addresscode, moshtarak.Name, moshtarak.Family, moshtarak.Tel1, moshtarak.Tel2, moshtarak.Label1, moshtarak.Label2, _Eshterak,eshterak);


                //if (moshtarak.Eshterak=="NULL")
                //{
                //    query = string.Format(this.updateQuery, _TableName, _Eshterak, _Name, _Family, _Tel1, _Tel2, _Label1, _Label2,
                //    eshterak, moshtarak.Name, moshtarak.Family, moshtarak.Tel1, moshtarak.Tel2, moshtarak.Label1, moshtarak.Label2, _AddressCode, addresscode);
                //}

                MySqlCommand sqlCommand = new MySqlCommand(UpdateMoshtarakProcedure);
                SetSqlCommandParameter(sqlCommand, moshtarak);

                result = this.database.ExecuteProcedure(sqlCommand);
            }
            catch (Exception exp)
            {
                this.message += exp.Message;
            }
            return result;
        }




        /// <summary>
        /// delete moshtarak from database
        /// </summary>
        /// <param name="moshtarak"></param>
        /// <returns></returns>
        public int Delete(Model.Moshtarak moshtarak)
        {
            int result = -1;
            try
            {
                if (moshtarak == null || string.IsNullOrEmpty(moshtarak.Eshterak))
                {
                    this.message = "moshtarak va eshterak ra sahih vared konid.";
                    return result;
                }

                MySqlCommand sqlCommand = new MySqlCommand(DeleteMoshtarakProcedure);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue(P_Eshterak, moshtarak.Eshterak);
                sqlCommand.Parameters[P_Eshterak].Direction = ParameterDirection.Input;

                result = database.ExecuteProcedure(sqlCommand);

                //string query = string.Format(this.deleteQuery, _TableName, _Eshterak, moshtarak.Eshterak);
                //result = this.database.ExecuteQuery(query);
            }
            catch (Exception exp)
            {
                this.message = exp.Message;
            }
            return result;
        }



        /// <summary>
        /// return count of moshtarak in database
        /// </summary>
        /// <param name="moshtarak"></param>
        /// <returns></returns>
        public int Count(Moshtarak moshtarak)
        {
            if (moshtarak == null)
            {
                return 0;
            }

            MySqlCommand sqlCommand = new MySqlCommand(CountMoshtarakProcedure);
            SetSqlCommandParameter(sqlCommand, moshtarak);
            return database.Count(sqlCommand);

            //var query = string.Format(countQuery, _TableName, _AddressCode, moshtarak.AddressCode, _Eshterak, moshtarak.Eshterak,
            //    _Name, moshtarak.Name, _Family, moshtarak.Family, _Tel1, moshtarak.Tel1, _Tel2, moshtarak.Tel2, _Label1, moshtarak.Label1, _Label2, moshtarak.Label2);
            //return database.Count(query);
        }



        /// <summary>
        /// set mysql parameters
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="moshtarak"></param>
        void SetSqlCommandParameter(MySqlCommand sqlCommand, Moshtarak moshtarak)
        {
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue(P_Eshterak, moshtarak.Eshterak);
            sqlCommand.Parameters[P_Eshterak].Direction = ParameterDirection.Input;

            sqlCommand.Parameters.AddWithValue(P_Addresscode, moshtarak.AddressCode);
            sqlCommand.Parameters[P_Addresscode].Direction = ParameterDirection.Input;

            sqlCommand.Parameters.AddWithValue(P_Name, moshtarak.Name);
            sqlCommand.Parameters[P_Name].Direction = ParameterDirection.Input;

            sqlCommand.Parameters.AddWithValue(P_Family, moshtarak.Family);
            sqlCommand.Parameters[P_Family].Direction = ParameterDirection.Input;

            sqlCommand.Parameters.AddWithValue(P_Tel1, moshtarak.Tel1);
            sqlCommand.Parameters[P_Tel1].Direction = ParameterDirection.Input;

            sqlCommand.Parameters.AddWithValue(P_Tel2, moshtarak.Tel2);
            sqlCommand.Parameters[P_Tel2].Direction = ParameterDirection.Input;

            sqlCommand.Parameters.AddWithValue(P_Lable1, moshtarak.Label1);
            sqlCommand.Parameters[P_Lable1].Direction = ParameterDirection.Input;

            sqlCommand.Parameters.AddWithValue(P_Lable2, moshtarak.Label2);
            sqlCommand.Parameters[P_Lable2].Direction = ParameterDirection.Input;
        }



        /// <summary>
        /// set mysql parameter
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="moshtarak"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        void SetSqlCommandParameter(MySqlCommand sqlCommand, Moshtarak moshtarak, int limit, int offset)
        {

            SetSqlCommandParameter(sqlCommand, moshtarak);

            sqlCommand.Parameters.AddWithValue(P_Limit, limit);
            sqlCommand.Parameters[P_Limit].Direction = ParameterDirection.Input;

            sqlCommand.Parameters.AddWithValue(P_Offset, offset);
            sqlCommand.Parameters[P_Offset].Direction = ParameterDirection.Input;
        }

        public string Message { get { return this.message; } }

    }
}
