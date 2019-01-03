using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Contact;

namespace TeduCoreApp.Application.Interfaces
{
   public interface IContactService:IDisposable
    {
        ContactViewModel GetContact();

        void Add(ContactViewModel contact);

        void Update(ContactViewModel contact);

        void SaveChanges();
    }
}
