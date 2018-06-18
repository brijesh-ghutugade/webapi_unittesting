using System;
using System.Collections.Generic;
using TodoApi.Models;

namespace TodoApi.DataAccessLayer
{
    public interface IContactRepository : IDisposable
    {
        IEnumerable<Contact> GetContacts();
        Contact GetContactByID(long contactId);
        void InsertContact(Contact contact);
        void DeleteContact(long contactID);
        void UpdateContact(Contact contact);
        void Save();
    }
}