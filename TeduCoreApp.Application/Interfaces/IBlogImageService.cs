using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Blog;

namespace TeduCoreApp.Application.Interfaces
{
   public interface IBlogImageService:IDisposable
    {
        void Add(BlogImageViewModel blogImage);

        void Update(BlogImage blogImageDb);

        BlogImageViewModel GetById(int id);

        BlogImage GetByIdDb(int id);

        List<BlogImageViewModel> GetAllByBlogId(int blogId);

        void Delete(int id);

        
        void SaveChanges();
    }
}
