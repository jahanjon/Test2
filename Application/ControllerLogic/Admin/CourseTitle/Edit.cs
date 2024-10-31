using Application.ControllerLogic.Admin.CourseTitle.DTOs;
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

namespace Application.ControllerLogic.Admin.CourseTitle
{
    public class Edit
    {
        public class Command:IRequest<Result<Unit>>
        {
            public EditDto Dto { get; set; }
            public int CourseTitleId { get; set; }
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
                var entity = await _db.CourseTitle.FirstOrDefaultAsync(x => x.Id == request.CourseTitleId);
                if (entity == null)
                {
                    return Result<Unit>.Failure(-400, "شناسه سرفصل دوره یافت نشد ");
                }
                var isExsit = await _db.Courses.AnyAsync(x => x.Id == request.Dto.CourseId);
                if (!isExsit)
                {
                    return Result<Unit>.Failure(-400, "شناسه دوره یافت نشد ");
                }
                _map.Map(request.Dto, entity);
                _db.CourseTitle.Update(entity);
                await _db.SaveChangesAsync();
                return Result<Unit>.Success(200, " سرفصل دوره  با موفقیت ویراش شد ");
            }
        }
    }
}
