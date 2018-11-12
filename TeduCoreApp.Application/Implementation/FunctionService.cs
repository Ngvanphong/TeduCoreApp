using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.FunctionVm;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Application.Implementation
{
    public class FunctionService : IFunctionService
    {
        private IRepository<Function, string> _functionRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FunctionService(IRepository<Function, string> functionRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _functionRepository = functionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
                List<FunctionViewModel> query = _mapper.Map<List<FunctionViewModel>>(_functionRepository.FindAll(x => x.Status == Data.Enums.Status.Active)
                    .OrderBy(x => x.ParentId).ToList());
                return query;
            }
            else
            {
                List<FunctionViewModel> query = _mapper.Map<List<FunctionViewModel>>(_functionRepository.FindAll(x => x.Name.Contains(filter) && x.Status == Data.Enums.Status.Active)
                    .OrderBy(x => x.ParentId).ToList());
                return query;
            }
        }

        public List<FunctionViewModel> GetAllWithParentId(string parentId)
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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}