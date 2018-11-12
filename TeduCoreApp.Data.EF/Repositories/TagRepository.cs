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
            return listProducts.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();          
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