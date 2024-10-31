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

namespace Application.ControllerLogic.Public.Course
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<Domain.Course>>>
        {
            public PagedListInputDto Dto { get; set; }
            public FilterInputDto Input { get; set; }
            public int SubCategoryId { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<PagedList<Domain.Course>>>
        {
            private readonly DataContext _db;
            public Handler(DataContext context)
            {
                _db = context;
            }
            public async Task<Result<PagedList<Domain.Course>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var spliteds = request.Input.Filter?.Split(' ');
                var query = _db.Courses.Where(x => !x.IsHidden&& !x.IsDelete)
                    .Include(x => x.Coach)
                    .Include(x => x.SubCategory)
                    .ThenInclude(x => x.Category)
                    .OrderBy(x=>x.CreatedAt)
                    .AsNoTracking();
                if (request.Input.Filter != null && request.Input.Filter != "")
                {
                    foreach (var splited in spliteds)
                    {
                        query = query.Where(x => x.Title.Contains(splited));
                    }
                }
                if (request.SubCategoryId > 0)
                {

                    query = query.Where(x => x.SubCategoryId == request.SubCategoryId);
                }
                var list = await query.Select(x => new Domain.Course
                {
                    Id = x.Id,
                    Title = x.Title,
                    Banner=x.Banner,
                    BannerType=x.BannerType,
                    RoyeshLink=x.RoyeshLink,
                    Demo=x.Demo,
                    Centers=x.Centers,
                    TimeLine = x.TimeLine,
                    Url = x.Url,
                    SubCategoryId=x.SubCategoryId,
                    SubCategory = x.SubCategory,
                    CoachId=x.CoachId,
                    Coach = x.Coach
                }) .CreatePagedListAsync(request.Dto);

                return Result<PagedList<Domain.Course>>.Success(200, "عملیات با موفقیت انجام شد", list);
            }
        }
    }
}

