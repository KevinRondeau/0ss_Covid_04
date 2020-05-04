﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        public MainViewModel()
        {
            NewContactCommand = new RelayCommand(NewContact);
            SaveContactCommand = new RelayCommand(UpdateContact);
            SearchContactCommand = new RelayCommand(SearchContact);
            SelectedContact = PhoneBookBusiness.GetContactByID(1);
            GetAllContactsFromDataBase(); //Init Value sur les autres travaille
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




        private void UpdateContact(object c)
        {
            int update = PhoneBookBusiness.UpdateContact(SelectedContact);
            contacts= new ObservableCollection<ContactModel>(PhoneBookBusiness.GetAllContacts());
        }


    }
    
}