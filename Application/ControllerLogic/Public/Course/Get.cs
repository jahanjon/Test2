using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ControllerLogic.Public.Course
{
    public class Get
    {
        public class Query : IRequest<Result<Domain.Course>>
        {
            [Required]
            public string Url { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<Domain.Course>>
        {
            private readonly DataContext _db;
            public Handler(DataContext context)
            {
                _db = context;
            }
            public async Task<Result<Domain.Course>> Handle(Query request, CancellationToken cancellationToken)
            {

                var query = _db.Courses.Where(x => !x.IsHidden)
                    .Include(x => x.Coach)
                    .Include(x => x.SubCategory)
                    .ThenInclude(x => x.Category)
                    .Include(x => x.CourseTitles)
                    .AsNoTracking();
                var get = await query.Select(x => new Domain.Course
                {
                    Id = x.Id,
                    Title = x.Title,
                    Banner = x.Banner,
                    BannerType = x.BannerType,
                    RoyeshLink = x.RoyeshLink,
                    Centers = x.Centers,
                    Demo = x.Demo,
                    Description = x.Description,
                    TimeLine = x.TimeLine,
                    Url = x.Url,
                    SubCategoryId = x.SubCategoryId,
                    SubCategory = x.SubCategory,
                    CoachId = x.CoachId,
                    Coach = x.Coach,
                    CourseTitles = x.CourseTitles

                }).Where(x => !x.SubCategory.Category.IsHidden && !x.SubCategory.IsHidden).FirstOrDefaultAsync(x => x.Url == request.Url);
                var isExist = await _db.Courses.AnyAsync(x => x.Url == request.Url);
                if (!isExist)
                {

                    return Result<Domain.Course>.Failure(-400, "آدرس اینترنتی یافت نشد");
                }
                return Result<Domain.Course>.Success(200, "عملیات با موفقیت انجام شد ", get);


            }
        }
    }
}
