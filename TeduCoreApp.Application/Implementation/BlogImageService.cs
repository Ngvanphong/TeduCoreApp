using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Blog;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Application.Implementation
{
   public class BlogImageService: IBlogImageService
    {
        private IRepository<BlogImage, int> _blogImageRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public BlogImageService(IRepository<BlogImage, int> blogImageRepository, IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _blogImageRepository = blogImageRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(BlogImageViewModel blogImage)
        {
            _blogImageRepository.Add(_mapper.Map<BlogImage>(blogImage));
        }

        public void Delete(int id)
        {
            _blogImageRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<BlogImageViewModel> GetAllByBlogId(int blogId)
        {
            return _mapper.Map<List<BlogImageViewModel>>(_blogImageRepository.FindAll(x => x.BlogId == blogId).OrderByDescending(x=>x.Id).ToList());
        }

        public BlogImageViewModel GetById(int id)
        {
           return _mapper.Map<BlogImageViewModel>(_blogImageRepository.FindById(id));
        }

        public BlogImage GetByIdDb(int id)
        {
            return _blogImageRepository.FindById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(BlogImage blogImageDb)
        {
            _blogImageRepository.Update(blogImageDb);
        }
    }
}
