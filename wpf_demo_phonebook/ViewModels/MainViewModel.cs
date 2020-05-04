using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using wpf_demo_phonebook.ViewModels.Commands;

namespace wpf_demo_phonebook.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private ObservableCollection<ContactModel> contacts;
        private ContactModel selectedContact;

        public ContactModel SelectedContact
        {
            get => selectedContact;
            set
            {
                selectedContact = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ContactModel> Contacts
        {
            get => contacts;
            private set
            {
                contacts = value;
                OnPropertyChanged();
            }
        }
       
        private string criteria;

        public string Criteria
        {
            get { return criteria; }
            set
            {
                criteria = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SearchContactCommand { get; set; }
        public RelayCommand NewContactCommand { get; set; }
        public RelayCommand SaveContactCommand { get; set; }
        public RelayCommand DeleteContactCommand { get; set; }
        public MainViewModel()
        {
            NewContactCommand = new RelayCommand(NewContact);
            SaveContactCommand = new RelayCommand(UpdateContact);
            SearchContactCommand = new RelayCommand(SearchContact);
            DeleteContactCommand = new RelayCommand(DeleteContact);
           
            GetAllContactsFromDataBase();
            SelectedContact = Contacts.First();
        }

        private void GetAllContactsFromDataBase()
        {
            Contacts = new ObservableCollection<ContactModel>(PhoneBookBusiness.GetAllContacts());
        }


        private void SearchContact(object parameter)
        {
            string input = parameter as string;
            int output;
            string searchMethod;
            if (!Int32.TryParse(input, out output))
            {
                searchMethod = "name";
            }
            else
            {
                searchMethod = "id";
            }

            switch (searchMethod)
            {
                case "id":
                    SelectedContact = PhoneBookBusiness.GetContactByID(output);
                   Contacts = new ObservableCollection<ContactModel>(PhoneBookBusiness.GetContactListByID(output));
                    break;
                case "name":
                    SelectedContact = PhoneBookBusiness.GetContactByName(input);
                    Contacts=new ObservableCollection<ContactModel>(PhoneBookBusiness.GetContactListByName(input));
                    break;
                default:
                    MessageBox.Show("Unkonwn search method");
                    break;
            }
        }

        private void NewContact(object c)
        {
            
            ContactModel contact = new ContactModel();
            SelectedContact = contact;
             
        }


        private void DeleteContact(object parameter)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Voulez-vous vraiment supprimer?", "Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                string input = parameter as string;

                int output;
                Int32.TryParse(input, out output);

                PhoneBookBusiness.Delete(output);

                GetAllContactsFromDataBase();
            }
        }

        private void UpdateContact(object c)
        {

            if (selectedContact.ContactID != 0)
            {
                int modif = PhoneBookBusiness.UpdateContact(SelectedContact);
            }
            else
            {
                int generatedNewId = PhoneBookBusiness.InsertContact(selectedContact);
                if (generatedNewId > 0)
                {
                    SelectedContact.ContactID = generatedNewId;
                    Contacts.Add(SelectedContact);

                    SelectedContact = Contacts.Last<ContactModel>();
                }
            }
            GetAllContactsFromDataBase();
        }


    }
    
}