using Application.ControllerLogic.Admin.Course.DTOs;
using Application.Core;
using AutoMapper;
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
    public class Create
    {
        public class Command:IRequest<Result<Unit>>
        {
            public CreateDto Dto { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _db;
            private readonly IMapper _map;
            public Handler(DataContext context,IMapper mapper)
            {
                _db = context;
                _map = mapper;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = _map.Map<CreateDto, Domain.Course>(request.Dto);
                entity.CreatedAt=DateTime.Now;
                var isExisit = await _db.SubCategories.AnyAsync(x => x.Id == entity.SubCategoryId);
                if (!isExisit)
                {
                    return Result<Unit>.Failure(-400, "شناسه زیر دسته یافت نشد ");
                }
                var isCoachId = await _db.Coaches.AnyAsync(x => x.Id == entity.CoachId);
                if (!isCoachId)
                {
                    return Result<Unit>.Failure(-400, "شناسه مربی یافت نشد ");
                }
                var url = await _db.Courses.AnyAsync(x => x.Url == request.Dto.Url);
                if (url)
                {
                    return Result<Unit>.Failure(-400, "ادرس اینترنت موجود است  ");
                }
                _db.Courses.Add(entity);
                await _db.SaveChangesAsync();
                return Result<Unit>.Success(200, "دوره با موفقیت ایجاد شد ");
            }
        }
    }
}
