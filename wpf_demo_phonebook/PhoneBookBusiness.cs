using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace wpf_demo_phonebook
{
    static class PhoneBookBusiness
    {
        private static PhonebookDAO dao = new PhonebookDAO();

        public static ContactModel GetContactByName(string _name)
        {
            ContactModel cm = null;

            DataTable dt = new DataTable();

            dt = dao.SearchByName(_name);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cm = RowToContactModel(row);
                }
            }

            return cm;
        }
        public static IEnumerable<ContactModel> GetContactListByName(string _name)
        {
            ContactModel cm = null;

            DataTable dt = dao.GetAllContact();

            dt = dao.SearchByName(_name);
            List<ContactModel> contactsList = new List<ContactModel> { };

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cm = RowToContactModel(row);
                    contactsList.Add(cm);
                }
            }

            foreach (ContactModel c in contactsList)
            {

                yield return c;
            }
        }

        public static IEnumerable<ContactModel> GetAllContacts()
        {
            ContactModel cm = null;

            DataTable dt = new DataTable();

            List<ContactModel> contactsList = new List<ContactModel> { };

            dt = dao.GetAllContact();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cm = RowToContactModel(row);

               //     Debug.WriteLine(cm.FirstName);   // Pour vérifier le contionement de la requete                
                  contactsList.Add(cm); 
                }


            }

            foreach (ContactModel c in contactsList)
            {
        
                yield return c;
            }


        }

        public static ContactModel GetContactByID(int _id)
        {
            ContactModel cm = null;

            DataTable dt = new DataTable();

            dt = dao.SearchByID(_id);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cm = RowToContactModel(row);
                }
            }

            return cm;
        }

        public static IEnumerable<ContactModel> GetContactListByID(int _id)
        {
            ContactModel cm = null;

            DataTable dt = dao.GetAllContact();
            List<ContactModel> contactsList = new List<ContactModel> { };

            dt = dao.SearchByID(_id);

            if (dt != null)
            {
             
                    foreach (DataRow row in dt.Rows)
                    {
                        cm = RowToContactModel(row);
                        contactsList.Add(cm);
                    }
                foreach (ContactModel c in contactsList)
                {

                    yield return c;
                }
            }
            
           
        }

        public static int UpdateContact(ContactModel cm)
        {
            int updates;
            if (cm != null)
            {
                int _id = cm.ContactID;
                updates = dao.Update(cm, _id);
            }

            return updates;
        }

        private static ContactModel RowToContactModel(DataRow row)
        {
            ContactModel cm = new ContactModel();

            cm.ContactID = Convert.ToInt32(row["ContactID"]);
            cm.FirstName = row["FirstName"].ToString();
            cm.LastName = row["LastName"].ToString();
            cm.Email = row["Email"].ToString();
            cm.Phone = row["Phone"].ToString();
            cm.Mobile = row["Mobile"].ToString();

            return cm;
        }
    }
}