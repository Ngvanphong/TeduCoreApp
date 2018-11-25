using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;
using TeduCoreApp.Data.ViewModels.FunctionVm;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Application.Implementation
{
    public class FunctionService : IFunctionService
    {
        private IFunctionRepository _functionRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FunctionService(IFunctionRepository functionRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _functionRepository = functionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool CheckExistedId(string id)
        {
            Function function = _functionRepository.FindById(id);
            if (function != null)
            {
                return true;
            }
            return false;
        }

        public void Create(FunctionViewModel functionVm)
        {
            _functionRepository.Add(_mapper.Map<Function>(functionVm));
        }

        public void Delete(string id)
        {
            _functionRepository.Remove(_functionRepository.FindById(id));
        }

        public FunctionViewModel Get(string id)
        {
            return _mapper.Map<FunctionViewModel>(_functionRepository.FindById(id));
        }

        public List<FunctionViewModel> GetAll(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                List<FunctionViewModel> query = _mapper.Map<List<FunctionViewModel>>(_functionRepository.FindAll()
                    .OrderBy(x => x.ParentId).ToList());
                return query;
            }
            else
            {
                List<FunctionViewModel> query = _mapper.Map<List<FunctionViewModel>>(_functionRepository.FindAll(x => x.Name.Contains(filter))
                    .OrderBy(x => x.ParentId).ToList());
                return query;
            }
        }

        public List<FunctionViewModel> GetAllWithParentId(string parentId)
        {
            return _mapper.Map<List<FunctionViewModel>>(_functionRepository.FindAll(x => x.ParentId == parentId).ToList());
        }

        public List<FunctionViewModel> GetAllWithPermission(string userId)
        {
           return _mapper.Map<List<FunctionViewModel>>(_functionRepository.GetListFunctionWithPermission(userId));
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(FunctionViewModel functionVm)
        {
            _functionRepository.Update(_mapper.Map<Function>(functionVm));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void Add(FunctionViewModel functionVm)
        {
            _functionRepository.Add(_mapper.Map<Function>(functionVm));           
        }
    }
}