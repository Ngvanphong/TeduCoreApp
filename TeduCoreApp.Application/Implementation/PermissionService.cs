using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.IRepositories;
using TeduCoreApp.Data.ViewModels.Permission;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Application.Implementation
{
    public class PermissionService : IPermissionService
    {
        IPermissionRepository _permissionRepository;
        IUnitOfWork _unitOfWork;

        public PermissionService(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork)
        {
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
        }
        public void Add(PermissionViewModel permission)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll(string functionId)
        {
            throw new NotImplementedException();
        }

        public void DeleteAllByRoleID(string roleID)
        {
            throw new NotImplementedException();
        }

        public ICollection<PermissionViewModel> GetByFunctionId(string functionId)
        {
            throw new NotImplementedException();
        }

        public ICollection<PermissionViewModel> GetByUserId(Guid userId)
        {
            return Mapper.Map<ICollection<PermissionViewModel>>(_permissionRepository.GetByUserId(userId));
        }

        public void SaveChange()
        {
            throw new NotImplementedException();
        }
    }
}
