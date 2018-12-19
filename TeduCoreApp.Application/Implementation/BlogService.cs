using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;
using TeduCoreApp.Data.ViewModels.Blog;
using TeduCoreApp.Infrastructure.Interfaces;
using TeduCoreApp.Utilities.Constants;
using TeduCoreApp.Utilities.Helpers;


namespace TeduCoreApp.Application.Implementation
{
    public class BlogService : IBlogService
    {
        private IRepository<Blog, int> _blogRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ITagRepository _tagRepository;
        private IRepository<BlogTag, int> _blogTagRepository;

        public BlogService(IRepository<Blog, int> blogRepository, IUnitOfWork unitOfWork, IMapper mapper,
            ITagRepository tagRepository, IRepository<BlogTag, int> blogTagRepository)
        {
            _blogRepository = blogRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tagRepository = tagRepository;
            _blogTagRepository = blogTagRepository;

        }
        public int Add(BlogViewModel blogVm)
        {
            Blog blogDb = _mapper.Map<Blog>(blogVm);
            _blogRepository.Add(blogDb);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(blogDb.Tags))
            {
                string[] listTag = blogDb.Tags.Split(',');
                for (int i = 0; i < listTag.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(listTag[i]);
                    if (_tagRepository.FindById(tagId) == null)
                    {
                        Tag tag = new Tag()
                        {
                            Id = tagId,
                            Name = listTag[i],
                            Type = CommonConstants.BlogTag,
                        };
                        _tagRepository.Add(tag);
                    }
                    BlogTag blogTag = new BlogTag()
                    {
                        BlogId = blogDb.Id,
                        TagId = tagId,
                    };
                    _blogTagRepository.Add(blogTag);
                }
            }
              return blogDb.Id;
        }

        public void Delete(int id)
        {
            _blogRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<BlogViewModel> GetAll()
        {
            return _mapper.Map<List<BlogViewModel>>(_blogRepository.FindAll(x=>x.Status==Data.Enums.Status.Active).OrderByDescending(x=>x.DateCreated).ToList());
        }

        public List<BlogViewModel> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            throw new NotImplementedException();
        }

        public List<BlogViewModel> GetAllPaging(string filter,int page, int pageSize, out int totalRow)
        {
            IQueryable<Blog> listBlog = _blogRepository.FindAll();
            if (!string.IsNullOrEmpty(filter))
            {
                listBlog = listBlog.Where(x => x.Name.Contains(filter));
            }
            totalRow = listBlog.Count();
            listBlog = listBlog.OrderByDescending(x => x.DateCreated).Skip((page - 1) * pageSize).Take(pageSize);
            return _mapper.Map<List<BlogViewModel>>(listBlog.ToList());
        }

        public BlogViewModel GetById(int id)
        {
            return _mapper.Map<BlogViewModel>(_blogRepository.FindById(id));
        }

        public Blog GetByIdDb(int id)
        {
            return _blogRepository.FindById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Blog blogDb)
        {
            _blogRepository.Update(blogDb);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(blogDb.Tags))
            {
                DeleteBlogTagByBlogId(blogDb.Id);
                _unitOfWork.Commit();
                string[] listTag = blogDb.Tags.Split(',');
                for (int i = 0; i < listTag.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(listTag[i]);
                    if (_tagRepository.FindById(tagId) == null)
                    {
                        Tag tag = new Tag()
                        {
                            Id = tagId,
                            Name = listTag[i],
                            Type = CommonConstants.BlogTag,
                        };
                        _tagRepository.Add(tag);
                    }
                    BlogTag blogTag = new BlogTag()
                    {
                        BlogId = blogDb.Id,
                        TagId = tagId,
                    };
                    _blogTagRepository.Add(blogTag);
                }
            }
        }
     

        private void DeleteBlogTagByBlogId(int blogId)
        {
            List<BlogTag> listBlogTags = _blogTagRepository.FindAll(x => x.BlogId == blogId).ToList();
            _blogTagRepository.RemoveMultiple(listBlogTags);
        }
    }
}
