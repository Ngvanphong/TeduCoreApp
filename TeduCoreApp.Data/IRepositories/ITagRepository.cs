using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Data.IRepositories
{
   public interface ITagRepository:IRepository<Tag, string>
    {
        List<Tag> GetTagByProductId(int productId);
        List<Product> GetProductAllByTag(string tagId, int pageIndex, int pageSize, out int totalRow);

        List<Blog> GetBlogByTag(string tagId, int pageIndex, int pageSize, out int totalRow);
        List<Tag> GetTagByBlogId(int blogId);


    }
}
