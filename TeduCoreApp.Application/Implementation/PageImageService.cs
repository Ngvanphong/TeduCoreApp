using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Page;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Application.Implementation
{
    public class PageImageService : IPageImageService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IRepository<PageImage, int> _pageImageRepository;
        public PageImageService(IUnitOfWork unitOfWork, IMapper mapper, IRepository<PageImage, int> pageImageRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _pageImageRepository = pageImageRepository;
        }
        public void Add(PageImageViewModel pageImage)
        {
            _pageImageRepository.Add(_mapper.Map<PageImage>(pageImage));
        }

        public void Delete(int id)
        {
            _pageImageRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<PageImageViewModel> GetAllByPageId(int pageId)
        {
            return _mapper.Map<List<PageImageViewModel>>(_pageImageRepository.FindAll(x => x.PageId == pageId).OrderByDescending(x=>x.Id).ToList());
        }

        public PageImageViewModel GetById(int id)
        {
            return _mapper.Map<PageImageViewModel>(_pageImageRepository.FindById(id));
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
