using Application.ControllerLogic.Admin.Course.DTOs;
using Application.Core;
using Application.Core.AppPage;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ControllerLogic.Admin.Course
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<CourseDto>>>
        {
            public PagedListInputDto Dto { get; set; }
            public FilterInputDto Input { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<PagedList<CourseDto>>>
        {
            private readonly DataContext _db;
            public Handler(DataContext context)
            {
                _db = context;
            }
            public async Task<Result<PagedList<CourseDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var spliteds = request.Input.Filter?.Split(' ');
                var query = _db.Courses
                    .Include(x=>x.Coach)
                    .Include(x=>x.SubCategory)
                    .ThenInclude(x=>x.Category)
                    .Where(x=>!x.IsDelete)
                    .AsNoTracking();
                if (request.Input.Filter != null && request.Input.Filter != "")
                {
                    foreach (var splited in spliteds)
                    {
                        query = query.Where(x => x.Title.Contains(splited));
                    }
                }
                var list = await query.Select(x => new CourseDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    TimeLine = x.TimeLine,
                    BannerType = x.BannerType,
                    Banner = x.Banner,
                    RoyeshLink=x.RoyeshLink,
                    Url = x.Url, 
                    Centers=x.Centers,
                    Demo=x.Demo,
                    IsHidden = x.IsHidden,
                    CoachId= x.CoachId,
                    CreatedAt = x.CreatedAt,
                    Coach=x.Coach,
                    SubCategoryId= x.SubCategoryId,
                    SubCategory=x.SubCategory
                   
                }).OrderBy(x => x.Id).ThenBy(x => x.Title).CreatePagedListAsync(request.Dto);
                return Result<PagedList<CourseDto>>.Success(200, "عملیات با موفقیت انجام شد ", list);
            }
        }
    }
}

