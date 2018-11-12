using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void DeleteAllByRoleId(string roleId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<PermissionViewModel> GetByFunctionId(string functionId)
        {
            throw new NotImplementedException();
        }

        public List<PermissionViewModel> GetByUserId(Guid userId)
        {
            return Mapper.Map<List<PermissionViewModel>>(_permissionRepository.GetByUserId(userId).ToList());
        }

        public void SaveChange()
        {
            throw new NotImplementedException();
        }
    }
}
