using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace wpf_demo_phonebook
{
    class PhonebookDAO
    {
        private DbConnection conn;

        public PhonebookDAO()
        {
            conn = new DbConnection();

        }

        /// <summary>
        /// Méthode permettant de rechercher un contact par nom
        /// </summary>
        /// <param name="_name">Nom de famille ou prénom</param>
        /// <returns>Une DataTable</returns>
        public DataTable SearchByName(string _name)
        {
            string _query =
                $"SELECT * " +
                $"FROM [Contacts] " +
                $"WHERE FirstName LIKE @firstName OR LastName LIKE @lastName ";

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@firstName", SqlDbType.NVarChar);
            parameters[0].Value = _name;

            parameters[1] = new SqlParameter("@lastName", SqlDbType.NVarChar);
            parameters[1].Value = _name;

            return conn.ExecuteSelectQuery(_query, parameters);
        }

        /// <summary>
        /// Méthode permettant de rechercher un contact par id
        /// </summary>
        /// <param name="_name">Nom de famille ou prénom</param>
        /// <returns>Une DataTable</returns>
        public DataTable SearchByID(int _id)
        {
            string _query =
                $"SELECT * " +
                $"FROM [Contacts] " +
                $"WHERE ContactID = @_id ";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@_id", SqlDbType.Int);
            parameters[0].Value = _id;

            return conn.ExecuteSelectQuery(_query, parameters);
        }

        public int Update(ContactModel cm, int _id)
        {
            string _query = $"UPDATE Contacts " +
                            $"SET FirstName = '{cm.FirstName}', " +
                            $"LastName = '{cm.LastName}'," +
                            $"Email = '{cm.Email}'," +
                            $"Phone = '{cm.Phone}'," +
                            $"Mobile = '{cm.Mobile}'" +
                            $"WHERE ContactID = @_id";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@_id", SqlDbType.Int);
            parameters[0].Value = _id;
            
            return conn.ExecutUpdateQuery(_query, parameters);
        }

        public int Insert(ContactModel cm)
        {
            string _query = $"INSERT INTO [Contacts] " +
                             $"(FirstName, LastName, Email, Phone, Mobile) " +
                            $"OUTPUT INSERTED.ContactID " +
                            $"VALUES ('{cm.FirstName}','{cm.LastName}','{cm.Email}','{cm.Phone}','{cm.Mobile}')";

            return conn.ExecutInsertQuery(_query, null);
        }

        public void Delete(int _id)
        {
            string _querry =
                $"DELETE FROM [Contacts] WHERE ContactID = @_id";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@_id", SqlDbType.Int);
            parameters[0].Value = _id;

            conn.ExecuteSelectQuery(_querry, parameters);
        }
        public DataTable GetAllContact()
        {

            string _querry =
                $"SELECT * FROM [Contacts]";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters = null;

            return conn.ExecuteSelectQuery(_querry, parameters);
        }
        public int LastID()
        {
            string _query = $"SELECT max(ContactID) " +
                            $"FROM [Contacts] ";

            return conn.ExecuteSelectQuery(_query, null).Rows[0].Field<int>(0);
        }
    }
}