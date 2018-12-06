﻿using System;
using System.Collections.Generic;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Bill;
using TeduCoreApp.Data.ViewModels.BillUserAnnoucement;

namespace TeduCoreApp.Application.Interfaces
{
    public interface IBillUserAnnoucementService : IDisposable
    {
        void Add(BillUserAnnoucementViewModel billUserAnnoucementVm);

        void AddDb(BillUserAnnoucement billUserAnnoucement);

        BillUserAnnoucementViewModel GetById(int id);

        List<BillViewModel> ListAllUnread(string userId);

        List<BillUserAnnoucementViewModel> GetAll();

        List<BillUserAnnoucementViewModel> GetAllByHasRead();

        void Delete(int id);

        void SaveChanges();
    }
}