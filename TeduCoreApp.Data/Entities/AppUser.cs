using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Data.ViewModels.Identity;

namespace TeduCoreApp.Data.Entities
{
    [Table("AppUsers")]
    public class AppUser : IdentityUser<Guid>, IDateTracking, ISwitchable
    {
        public AppUser():base()
        {

        }
        public AppUser(AppUserViewModel appUserVm) : base(appUserVm.UserName)
        {
            FullName = appUserVm.FullName;
            BirthDay = appUserVm.BirthDay;
            Balance = appUserVm.Balance;
            Avatar = appUserVm.Avatar;
            Status = appUserVm.Status;
            PhoneNumber = appUserVm.PhoneNumber;
            Email = appUserVm.Email;
            DateCreated = appUserVm.DateCreated;
            DateModified = appUserVm.DateModified;         
        }

        public string FullName { get; set; }

        public DateTime? BirthDay { set; get; }

        public decimal Balance { get; set; }

        public string Avatar { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public Status Status { get; set; }

        
    }
}
