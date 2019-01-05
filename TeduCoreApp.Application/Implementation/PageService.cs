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
    public class PageService : IPageService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IRepository<Page, int> _pageRepository;
        public PageService(IUnitOfWork unitOfWork, IMapper mapper, IRepository<Page, int> pageRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _pageRepository = pageRepository;
        }
        public int Add(PageViewModel pageVm)
        {
            Page page = _mapper.Map<Page>(pageVm);
            _pageRepository.Add(page);
            _unitOfWork.Commit();
            return page.Id;          
        }

        public void Delete(int id)
        {
            _pageRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<PageViewModel> GetAll()
        {
            return _mapper.Map<List<PageViewModel>>(_pageRepository.FindAll().ToList());
        }

        public PageViewModel GetAllPaggingByAlias(string alias)
        {
            return _mapper.Map<PageViewModel>(_pageRepository.FindSingle(x => x.Alias == alias));
        }

        public PageViewModel GetById(int id)
        {
            return _mapper.Map<PageViewModel>(_pageRepository.FindById(id));
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(PageViewModel pageVm)
        {
            _pageRepository.Update(_mapper.Map<Page>(pageVm));
        }
    }
}
