using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Infrastructure.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using TeduCoreApp.Data.ViewModels.FunctionVm;

namespace TeduCoreApp.Application.Implementation
{
    public class FunctionService : IFunctionService
    {
        private IRepository<Function,string> _functionRepository;
        private IUnitOfWork _unitOfWork;

        public FunctionService(IRepository<Function, string> functionRepository, IUnitOfWork unitOfWork)
        {
          _functionRepository = functionRepository;
           _unitOfWork = unitOfWork;
        }
        public bool CheckExistedId(string id)
        {
            throw new NotImplementedException();
        }

        public FunctionViewModel Create(FunctionViewModel functionVm)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public FunctionViewModel Get(string id)
        {
            throw new NotImplementedException();
        }

        public List<FunctionViewModel> GetAll(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                List<FunctionViewModel> query = _functionRepository.FindAll(x => x.Status).OrderBy(x => x.ParentId).ProjectTo<FunctionViewModel>().ToList();
                return query;
            }
            else
            {
                List<FunctionViewModel> query = _functionRepository.FindAll(x =>x.Name.Contains(filter)&&x.Status==Data.Enums.Status.Active ).OrderBy(x => x.ParentId)
                    .ProjectTo<FunctionViewModel>().ToList();
                return query;
            }
        }

        public List<FunctionViewModel> GetAllWithParentID(string parentId)
        {
            throw new NotImplementedException();
        }

        public List<FunctionViewModel> GetAllWithPermission(string userId)
        {
            throw new NotImplementedException();
        }

        public void SaveChange()
        {
            throw new NotImplementedException();
        }

        public void Update(FunctionViewModel functionVm)
        {
            throw new NotImplementedException();
        }
    }
}
