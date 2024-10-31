using Application.ControllerLogic.Admin.Course.DTOs;
using Application.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ControllerLogic.Admin.Course
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public EditDto Dto { get; set; }
            [Required]
            public int CourseId { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _db;
            private readonly IMapper _map;
            public Handler(DataContext conetx,IMapper mapper)
            {
                _db=conetx;
                _map=mapper;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = await _db.Courses.FirstOrDefaultAsync(x => x.Id == request.CourseId);
                if (entity == null)
                {
                    return Result<Unit>.Failure(-400, "شناسه دوره یافت نشد ");
                }
                var isExisit = await _db.SubCategories.AnyAsync(x => x.Id == request.Dto.SubCategoryId);
                if (!isExisit)
                {
                    return Result<Unit>.Failure(-400, "شناسه زیر دسته یافت نشد ");
                }
                var isCoachId = await _db.Coaches.AnyAsync(x => x.Id == request.Dto.CoachId);
                if (!isCoachId)
                {
                    return Result<Unit>.Failure(-400, "شناسه مربی یافت نشد ");
                }
                _map.Map(request.Dto, entity);
                _db.Courses.Update(entity);
                await _db.SaveChangesAsync();
                return Result<Unit>.Success(200, "دروه  با موفقیت ویراش شد ");
            }
        }
    }
}
