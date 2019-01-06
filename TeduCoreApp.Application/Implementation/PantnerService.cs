using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Pantner;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Application.Implementation
{
    public class PantnerService : IPantnerService
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<Pantner, int> _pantnerRepository;
        private IMapper _mapper;
        public PantnerService(IUnitOfWork unitOfWork, IRepository<Pantner, int> pantnerRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _pantnerRepository = pantnerRepository;
            _mapper = mapper;
        }
        public void Add(PantnerViewModel pantnerVm)
        {
            _pantnerRepository.Add(_mapper.Map<Pantner>(pantnerVm));
        }

        public void Delete(int id)
        {
            _pantnerRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<PantnerViewModel> GetAll()
        {
            return _mapper.Map<List<PantnerViewModel>>(_pantnerRepository.FindAll().OrderBy(x=>x.Id).ToList());
        }

        public List<PantnerViewModel> GetAllStatus()
        {
            return _mapper.Map<List<PantnerViewModel>>(_pantnerRepository.FindAll(x=>x.Status==Data.Enums.Status.Active).OrderBy(x=>x.Name).ToList());
        }

        public PantnerViewModel GetById(int id)
        {
            return _mapper.Map<PantnerViewModel>(_pantnerRepository.FindById(id));
        }

        public Pantner GetByIdDb(int id)
        {
            return _pantnerRepository.FindById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Pantner pantnerDb)
        {
            _pantnerRepository.Update(pantnerDb);
        }
    }
}
