using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
  
    public static class Paging
    {
        private static int firstPage = 1;
        private static int lastpage=3;
        private static int ttlPagingTagsVisible = 3;
        private static int totalPages;

        public static int Firstpage
        {
            get
            {
                return firstPage;
            }
            set
            {
                firstPage = value;
            }
        }

        public static int LastPage
        {
            get 
            {
                return lastpage;
            }
            set
            {
                lastpage = value;
            }
        }

        public static int TtlPagingTagsVisible
        {
            get
            {
                return ttlPagingTagsVisible;
            }
            set
            {
                ttlPagingTagsVisible = value;
            }
        }

        public static int TotalPages
        {
            get
            {
                return totalPages;
            }
            set
            {
                totalPages = value;
            }
        }

        internal static void calculatePageNos(int page)
        {

            if (totalPages - page >= 3 || totalPages - page == 2)
            {
                ttlPagingTagsVisible = 3;
                firstPage = page;
                lastpage = page + 2;
            }
            else if (totalPages - page == 1)
            {
                ttlPagingTagsVisible = 2;
                firstPage = page;
                lastpage = page + 1;
            }
            else if (totalPages - page == 0)
            {
                ttlPagingTagsVisible = 1;
                firstPage = page;
                lastpage = page;
            }

            Firstpage = firstPage;
            LastPage = lastpage;
            TtlPagingTagsVisible = ttlPagingTagsVisible;
        }
    }


    public partial class drpDetails
    {
        public drpDetails(Int32? id, string name, Boolean status)
        {
            this.ID = id;
            this.Name = name;
            this.Status = status;
        }
        public int? ID { get; set; }
        public string Name { get; set; }
        public Boolean? Status { get; set; }

    }

    public class rangesDetail
    {
        //public Int32 Device { get; set; }
        //public Int32 Product { get; set; }
        public string RangeStart { get; set; }
        public string RangeEnd { get; set; }
        public string RegularExpression { get; set; }
        public string IsLatest { get; set; }
    }

}