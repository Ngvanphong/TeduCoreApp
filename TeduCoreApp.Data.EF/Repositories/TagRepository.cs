using System.Collections.Generic;
using System.Linq;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;
using TeduCoreApp.Data.ViewModels.Product;

namespace TeduCoreApp.Data.EF.Repositories
{
    public class TagRepository : EFRepository<Tag, string>, ITagRepository
    {
        private AppDbContext _context;
        public TagRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Blog> GetBlogByTag(string tagId, int pageIndex, int pageSize, out int totalRow)
        {
            List<Blog> listBlogs = (from b in _context.Blogs
                                          join
                                           bt in _context.BlogTags
                                           on b.Id equals bt.BlogId
                                          where bt.TagId == tagId
                                          orderby b.DateCreated descending
                                          select b).ToList();
            totalRow = listBlogs.Count();
            return listBlogs.OrderByDescending(x=>x.DateCreated).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Product> GetProductAllByTag(string tagId, int pageIndex, int pageSize, out int totalRow)
        {
            List<Product> listProducts = (from p in _context.Products
                                          join
                                           pt in _context.ProductTags
                                           on p.Id equals pt.ProductId
                                          where pt.TagId == tagId
                                          orderby p.DateCreated descending
                                          select p).ToList();
            totalRow = listProducts.Count();
            return listProducts.OrderByDescending(x=>x.DateCreated).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();          
        }

        public List<Tag> GetTagByBlogId(int blogId)
        {
            List<Tag> listTags = (from t in _context.Tags
                                  join
                                   bt in _context.BlogTags
                                   on t.Id equals bt.TagId
                                  where bt.BlogId == blogId
                                  orderby t.Name
                                  select t).ToList();
            return listTags;
        }

        public List<Tag> GetTagByProductId(int productId)
        {
            List<Tag> listTags = (from t in _context.Tags
                                  join
                                   pt in _context.ProductTags
                                   on t.Id equals pt.TagId
                                  where pt.ProductId == productId
                                  orderby t.Name
                                  select t).ToList();
            return listTags;
        }
    }
}