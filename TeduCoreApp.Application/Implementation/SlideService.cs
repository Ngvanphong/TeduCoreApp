using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Slide;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Application.Implementation
{
    public class SlideService : ISlideService
    {
        private IRepository<Slide, int> _slideRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public SlideService(IRepository<Slide, int> slideRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _slideRepository = slideRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(SlideViewModel slideVm)
        {
            _slideRepository.Add(_mapper.Map<Slide>(slideVm));
        }

        public void Delete(int id)
        {
            _slideRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<SlideViewModel> GetAllPagging(int page,int pageSize, string filter,out int totalRow)
        {
            var slides = _slideRepository.FindAll();
            if (!string.IsNullOrEmpty(filter))
            {
                slides = slides.Where(x => x.Name.Contains(filter));
            }
            totalRow = slides.Count();
            slides = slides.OrderByDescending(x=>x.OrtherPageHome).Skip((page - 1) * pageSize).Take(pageSize);
            return _mapper.Map<List<SlideViewModel>>(slides.ToList());

        }

        public SlideViewModel GetById(int id)
        {
            return _mapper.Map<SlideViewModel>(_slideRepository.FindById(id));
        }

        public Slide GetByIdDb(int id)
        {
            return _slideRepository.FindById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Slide slideDb)
        {
            _slideRepository.Update(slideDb);
        }
    }
}
