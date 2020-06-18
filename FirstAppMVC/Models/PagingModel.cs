using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAppMVC.Models
{
    public class PagingModel
    {
        public int CurrentPage { get; set; }
        public int PagesCount { get; set; }
        public bool HaveNextPage => CurrentPage < PagesCount;
        public bool HavePreviousPage => CurrentPage > 1;

        public PagingModel(int elementsCount, int pagesSize, int currentPage)
        {
            CurrentPage = currentPage;
            PagesCount = (int) Math.Ceiling(elementsCount / (double)pagesSize);
        }
    }
}
