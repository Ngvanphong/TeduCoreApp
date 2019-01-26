using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Utilities.Constants;

namespace TeduCoreApp.Data.EF
{
    public class DbInitializer
    {
        private readonly AppDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;

        public DbInitializer(AppDbContext context,UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;       
        }
        public async Task Seed()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Description = "Top manager"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Staff",
                    NormalizedName = "Staff",
                    Description = "Staff"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Customer",
                    NormalizedName = "Customer",
                    Description = "Customer"
                });
            }
            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new AppUser()
                {
                    UserName = "ngvanphong",
                    FullName = "Nguyễn Văn Phong",
                    Email = "ngvanphong2012@gmail.com",
                    Balance = 0,
                }, "vanphong2012");
                var user = await _userManager.FindByNameAsync("ngvanphong"); 
                
                await _userManager.AddToRoleAsync(user, "Admin");
                                            
            }
            if (_context.Functions.Count() == 0)
            {
                _context.Functions.AddRange(new List<Function>()
                {
                    new Function() {Id = "SYSTEM", Name = "Hệ thống",ParentId = null,SortOrder = 1,Status = Status.Active,URL = "/",IconCss = "fa-desktop"  },
                    new Function() {Id = "ROLE", Name = "Nhóm",ParentId = "SYSTEM",SortOrder = 1,Status = Status.Active,URL = "/main/role/index",IconCss = "fa-home"  },
                    new Function() {Id = "FUNCTION", Name = "Chức năng",ParentId = "SYSTEM",SortOrder = 2,Status = Status.Active,URL = "/main/function/index",IconCss = "fa-home"  },
                    new Function() {Id = "USER", Name = "Người dùng",ParentId = "SYSTEM",SortOrder =3,Status = Status.Active,URL = "/main/user/index",IconCss = "fa-home"  },
                   
                    new Function() {Id = "PRODUCT",Name = "Sản phẩm",ParentId = null,SortOrder = 2,Status = Status.Active,URL = "/",IconCss = "fa-chevron-down"  },
                    new Function() {Id = "PRODUCT_CATEGORY",Name = "Danh mục",ParentId = "PRODUCT",SortOrder =1,Status = Status.Active,URL = "/main/productcategory/index",IconCss = "fa-chevron-down"  },
                    new Function() {Id = "PRODUCT_LIST",Name = "Sản phẩm",ParentId = "PRODUCT",SortOrder = 2,Status = Status.Active,URL = "/main/product/index",IconCss = "fa-chevron-down"  },
                    new Function() {Id = "BILL",Name = "Hóa đơn",ParentId = "PRODUCT",SortOrder = 3,Status = Status.Active,URL = "/main/order/index",IconCss = "fa-chevron-down"  },
                   
                    new Function() {Id = "CONTENT",Name = "Nội dung",ParentId = null,SortOrder = 3,Status = Status.Active,URL = "/",IconCss = "fa-table"  },
                    new Function() {Id = "BLOG",Name = "Bài viết",ParentId = "CONTENT",SortOrder = 1,Status = Status.Active,URL = "/main/post/index",IconCss = "fa-table"  },

                    new Function() {Id = "UTILITY",Name = "Tiện ích",ParentId = null,SortOrder = 4,Status = Status.Active,URL = "/",IconCss = "fa-clone"  },
                    new Function() {Id = "CONTACT",Name = "Liên hệ",ParentId = "UTILITY",SortOrder = 1,Status = Status.Active,URL = "/main/contact/index",IconCss = "fa-clone"  },
                    new Function() {Id = "SLIDE",Name = "Slide",ParentId = "UTILITY",SortOrder = 2,Status = Status.Active,URL = "/main/slide/index",IconCss = "fa-clone"  },
                    new Function() {Id = "ADVERTISMENT",Name = "Quảng cáo",ParentId = "UTILITY",SortOrder = 3,Status = Status.Active,URL = "/main/advertistment/index",IconCss = "fa-clone"  },
                    new Function() {Id = "PAGE",Name = "Trang lẻ",ParentId = "UTILITY",SortOrder = 4,Status = Status.Active,URL = "/main/page/index",IconCss = "fa-clone"  },
                    new Function() {Id = "PANTNER",Name = "Đối tác",ParentId = "UTILITY",SortOrder = 5,Status = Status.Active,URL = "/main/pantner/index",IconCss = "fa-clone"  },
                    new Function() {Id = "SYSTEMCONFIG",Name = "Cấu hình Seo",ParentId = "UTILITY",SortOrder = 6,Status = Status.Active,URL = "/main/systemconfig/index",IconCss = "fa-clone"  },
                    new Function() {Id = "SENDEMAIL",Name = "Gửi mail",ParentId = "UTILITY",SortOrder = 7,Status = Status.Active,URL = "/main/sendemail/index",IconCss = "fa-clone"  },
                    new Function() {Id = "TAG",Name = "Quản lý Tag",ParentId = "UTILITY",SortOrder = 8,Status = Status.Active,URL = "/main/tag/index",IconCss = "fa-clone"  },
                    new Function() {Id = "SIZE",Name = "Quản lý Size",ParentId = "UTILITY",SortOrder = 9,Status = Status.Active,URL = "/main/size/index",IconCss = "fa-clone"  },
                    new Function() {Id = "COLOR",Name = "Quản lý Color",ParentId = "UTILITY",SortOrder =10,Status = Status.Active,URL = "/main/color/index",IconCss = "fa-clone"  },

                    new Function() {Id = "REPORT",Name = "Báo cáo",ParentId = null,SortOrder = 5,Status = Status.Active,URL = "/",IconCss = "fa-bar-chart-o"  },
                    new Function() {Id = "REVENUE",Name = "Báo cáo doanh thu",ParentId = "REPORT",SortOrder = 1,Status = Status.Active,URL = "/main/revenue",IconCss = "fa-bar-chart-o"  },
                });
                _context.SaveChanges();
            }
            

            if (_context.Colors.Count() == 0)
            {
                List<Color> listColor = new List<Color>()
                {
                    new Color() {Name="Đen", Code="#000000" },
                    new Color() {Name="Trắng", Code="#FFFFFF"},
                    new Color() {Name="Đỏ", Code="#ff0000" },
                    new Color() {Name="Xanh", Code="#1000ff" },
                };
                _context.Colors.AddRange(listColor);
                _context.SaveChanges();
            }
           

            if (_context.Slides.Count() == 0)
            {
                List<Slide> slides = new List<Slide>()
                {
                    new Slide() {Name="Slide 1",Image="/client-side/images/slider/slide-1.jpg",Url="#",DisplayOrder = 0,OrtherPageHome = false,Status = true },
                    new Slide() {Name="Slide 2",Image="/client-side/images/slider/slide-2.jpg",Url="#",DisplayOrder = 1,OrtherPageHome = false,Status = true },
                    new Slide() {Name="Slide 3",Image="/client-side/images/slider/slide-3.jpg",Url="#",DisplayOrder = 2,OrtherPageHome = false,Status = true },
              
                };
                _context.Slides.AddRange(slides);
                _context.SaveChanges();
            }


            if (_context.Sizes.Count() == 0)
            {
                List<Size> listSize = new List<Size>()
                {
                    new Size() { Name="XXL" },
                    new Size() { Name="XL"},
                    new Size() { Name="L" },
                    new Size() { Name="M" },
                    new Size() { Name="S" },
                    new Size() { Name="XS" }
                };
                _context.Sizes.AddRange(listSize);
                _context.SaveChanges();
            }

            if (_context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
                {
                    new ProductCategory() { Name="Áo nam",SeoAlias="ao-nam",ParentId = null,Status=Status.Active,SortOrder=1,
                        Products = new List<Product>()
                        {
                            new Product(){Name = "Sản phẩm 1",SeoAlias = "san-pham-1",Price = 1000,Status = Status.Active,OriginalPrice = 1000,Content="Content 1"},                            
                        }
                    },
                    new ProductCategory() { Name="Áo nữ",SeoAlias="ao-nu",ParentId = null,Status=Status.Active ,SortOrder=2,
                        Products = new List<Product>()
                        {
                            new Product(){Name = "Sản phẩm 6",SeoAlias = "san-pham-6",Price = 1000,Status = Status.Active,OriginalPrice = 1000,Content="Content 6"},                           
                        }},
                    new ProductCategory() { Name="Giày nam",SeoAlias="giay-nam",ParentId = null,Status=Status.Active ,SortOrder=3,
                        Products = new List<Product>()
                        {
                            new Product(){Name = "Sản phẩm 11",SeoAlias = "san-pham-11",Price = 1000,Status = Status.Active,OriginalPrice = 1000,Content="Content 11"},                       
                        }},
                    new ProductCategory() { Name="Giày nữ",SeoAlias="giay-nu",ParentId = null,Status=Status.Active,SortOrder=4,
                        Products = new List<Product>()
                        {
                            new Product(){Name = "Sản phẩm 16",SeoAlias = "san-pham-16",Price = 1000,Status = Status.Active,OriginalPrice = 1000,Content="Content 16"},
                            
                        }}
                };
                _context.ProductCategories.AddRange(listProductCategory);
                _context.SaveChanges();
            }

            if (!_context.SystemConfigs.Any(x => x.Id == "HomeTitle"))
            {
                _context.SystemConfigs.Add(new SystemConfig()
                {
                    Id = "HomeTitle",
                    Name = "Tiêu đề trang chủ",
                    Value1 = "Trang chủ TeduShop",
                    Status = Status.Active
                });
                _context.SaveChanges();
            }
            if (!_context.SystemConfigs.Any(x => x.Id == "HomeMetaKeyword"))
            {
                _context.SystemConfigs.Add(new SystemConfig()
                {
                    Id = "HomeMetaKeyword",
                    Name = "Từ khoá trang chủ",
                    Value1 = "Trang chủ TeduShop",
                    Status = Status.Active
                });
                _context.SaveChanges();
            }
            if (!_context.SystemConfigs.Any(x => x.Id == "HomeMetaDescription"))
            {
                _context.SystemConfigs.Add(new SystemConfig()
                {
                    Id = "HomeMetaDescription",
                    Name = "Mô tả trang chủ",
                    Value1 = "Trang chủ TeduShop",
                    Status = Status.Active
                });
                _context.SaveChanges();
            }
        }
    }
}
