using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.EF.Repositories;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;
using TeduCoreApp.Data.ViewModels.Permission;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Application.Implementation
{
    public class PermissionService : IPermissionService
    {
        private IPermissionRepository _permissionRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;


        public PermissionService(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork,IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;           
        }
        public void Add(PermissionViewModel permission)
        {
            _permissionRepository.Add(_mapper.Map<Permission>(permission));          
        }

        public void AddDb(Permission permission)
        {
            _permissionRepository.Update(permission);
        }

      

        public void DeleteAll(string functionId)
        {
            List<Permission> listPermission= _permissionRepository.FindAll(x => x.FunctionId == functionId).ToList();
            _permissionRepository.RemoveMultiple(listPermission);
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
            return _mapper.Map<List<PermissionViewModel>>(_permissionRepository.FindAll(x => x.FunctionId == functionId,r=>r.AppRole).ToList());
        }

        public List<PermissionViewModel> GetByUserId(Guid userId)
        {
            return _mapper.Map<List<PermissionViewModel>>(_permissionRepository.GetByUserId(userId).ToList());
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
