using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.AppPage
{
    //public static class PagedListExtension
    //{
    //    public static async Task<PagedList<T>> CreatePagedListAsync<T>(this IQueryable<T> source, PagedListInputDto dto)
    //    {
    //        var count = await source.CountAsync();

    //        var items = await source.Skip((dto.PageNumber - 1) * dto.ItemsCountPerPage).Take(dto.ItemsCountPerPage).ToListAsync();

    //        return new PagedList<T>(items, count, dto.PageNumber, dto.ItemsCountPerPage);
    //    }
    //}
}
