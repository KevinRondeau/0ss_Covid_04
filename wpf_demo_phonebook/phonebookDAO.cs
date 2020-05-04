﻿using System;
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

        public DataTable UpdateByID(int _id,string _name,string _lastname,string _email, string _phone,string _mobile )
        {
            string _query =
                $"UPDATE Contacts " +
                $"SET FirstName=@_name,LastName=@_lastname,Email=@_email,Phone=@_phone,Mobile=@_mobile" +
                $"WHERE ContactID = @_id ";

            SqlParameter[] parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("@_id", SqlDbType.Int);
            parameters[0].Value = _id;
            parameters[1] = new SqlParameter("@_name", SqlDbType.NVarChar);
            parameters[1].Value = _name;
            parameters[2] = new SqlParameter("@_lastname", SqlDbType.NVarChar);
            parameters[2].Value = _lastname;
            parameters[3] = new SqlParameter("@_email", SqlDbType.NVarChar);
            parameters[3].Value = _email;
            parameters[4] = new SqlParameter("@_phone", SqlDbType.NVarChar);
            parameters[4].Value = _phone;
            parameters[5] = new SqlParameter("@_mobile", SqlDbType.NVarChar);
            parameters[5].Value = _mobile;

            return conn.ExecuteSelectQuery(_query, parameters);
        }

        public DataTable GetAllContact()
        {

            string _querry =
                $"SELECT * FROM [Contacts]";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters = null;

            return conn.ExecuteSelectQuery(_querry, parameters);
        }
    }
}