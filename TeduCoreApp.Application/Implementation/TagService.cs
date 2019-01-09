using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;
using TeduCoreApp.Data.ViewModels.Tag;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Application.Implementation
{
    public class TagService : ITagService
    {
        private ITagRepository _tagRepository;
        private IUnitOfWork _unitOfWork;
        private IRepository<ProductTag, int> _productTagRepository;
        private IRepository<BlogTag, int> _blogTagRepository;
        private IMapper _mapper;

        public TagService(ITagRepository tagRepository, IUnitOfWork unitOfWork,
            IRepository<ProductTag, int> productTagRepository, IRepository<BlogTag, int> blogTagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
            _productTagRepository = productTagRepository;
            _blogTagRepository = blogTagRepository;
            _mapper = mapper;
        }

        public void DeleteMultiNotUse()
        {
            var listProductTag = _productTagRepository.FindAll().Select(x => x.TagId);
            var listBlogTag = _blogTagRepository.FindAll().Select(x => x.TagId);
            var listTag = _tagRepository.FindAll().Select(x => x.Id);
            var listTagUse = listProductTag.Union(listBlogTag);
            var listTagNotUse = listTag.Except(listTagUse);
            foreach (var item in listTagNotUse)
            {
                _tagRepository.Remove(item);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<TagViewModel> GetAllPagging(int page, int pageSize, out int totalRows, string filter)
        {
            var query = _tagRepository.FindAll();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Name.Contains(filter));
            }
            totalRows = query.Count();
            query = query.OrderBy(x => x.Name).Skip((page - 1) * pageSize).Take(pageSize);
            return _mapper.Map<List<TagViewModel>>(query.ToList());
        }


        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}