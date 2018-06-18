using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.DataAccessLayer
{
    public class ContactRepository : IContactRepository, IDisposable
    {
        private ContactContext context;

        public ContactRepository(ContactContext context)
        {
            this.context = context;
        }

        public Contact GetContactByID(long id)
        {
            return context.Contacts.Find(id);
        }

        public IEnumerable<Contact> GetContacts()
        {
            return context.Contacts.ToList();
        }

        public void InsertContact(Contact contact)
        {
            context.Contacts.Add(contact);
        }

        public void DeleteContact(long contactID)
        {
            Contact contact = context.Contacts.Find(contactID);
            context.Contacts.Remove(contact);
        }

        public void UpdateContact(Contact contact)
        {
            context.Entry(contact).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}