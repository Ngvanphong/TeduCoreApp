using System;
using System.Collections.Generic;
using System.Text;

namespace TeduCoreApp.Utilities.Dtos
{
   public class ApiResultPaging<T>
    {
        public List<T> Items { get; set; }
        public int PageIndex { get; set; }      
        public int PageSize { set; get; }
        public int TotalRows { get; set; }      
        public int TotalPages
        {
            get { return (int)Math.Ceiling((double)(TotalRows / PageSize)); }
        }

    }
}
