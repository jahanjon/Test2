using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.AppPage
{
    public class PagedList<T>
    {
        public List<T> Items { get; set; }
        public int TotalPageNumber { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
    }
    
    public class PagedListInputDto
    {
        [Required]
        public int PageNumber { get; set; }
        [Required]
        public int ItemsCount { get; set; }
    }
    public class FilterInputDto
    {
   
        public string Filter { get; set; }
    }
    public static class PagedListExtension
    {
        public static async Task<PagedList<T>> CreatePagedListAsync<T>(this IQueryable<T> query, PagedListInputDto pagedListInput)
        {
            if (pagedListInput.PageNumber <= 0)
                pagedListInput.PageNumber = 1;
            if (pagedListInput.ItemsCount <= 0)
                pagedListInput.ItemsCount = 20;

            if (pagedListInput.ItemsCount > 200)
                pagedListInput.ItemsCount = 200;
            var totalItems = await query.CountAsync();
            var totalPage = (int)Math.Ceiling((double)totalItems / (double)pagedListInput.ItemsCount);
            var items = await query.Skip((pagedListInput.PageNumber - 1) * pagedListInput.ItemsCount).Take(pagedListInput.ItemsCount).ToListAsync();
            return new PagedList<T>
            {
                Items = items,
                CurrentPage = pagedListInput.PageNumber,
                TotalItems = totalItems,
                TotalPageNumber = totalPage
            };
        }
    }
}
