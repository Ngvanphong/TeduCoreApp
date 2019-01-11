using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Subcrible;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Application.Implementation
{
    public class SubcribleService : ISubcribleService
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<Subcrible, int> _subcribleRepository;
        private IMapper _mapper;

        public SubcribleService(IUnitOfWork unitOfWork, IRepository<Subcrible, int> subcribleRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _subcribleRepository = subcribleRepository;
            _mapper = mapper;
        }

        public void Add(string email)
        {
            Subcrible subcribleDb = new Subcrible()
            {
                Email = email
            };
            _subcribleRepository.Add(subcribleDb);
        }

        public bool CheckExit(string email)
        {
            Subcrible subcrible = _subcribleRepository.FindSingle(x => x.Email == email);
            if (subcrible != null) return true;
            return false;
        }

        public void Delete(int id)
        {
            _subcribleRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<SubcribleViewModel> GetAll()
        {
            return _mapper.Map<List<SubcribleViewModel>>(_subcribleRepository.FindAll().ToList());
        }

        public List<string> GetAllEmail()
        {
            return _subcribleRepository.FindAll().Select(x => x.Email).ToList();
        }

        public List<SubcribleViewModel> GetPaging(int page, int pageSize, out int totalRow)
        {
            var query = _subcribleRepository.FindAll();
            totalRow = query.Count();
            query = query.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
            return _mapper.Map<List<SubcribleViewModel>>(query.ToList());
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}