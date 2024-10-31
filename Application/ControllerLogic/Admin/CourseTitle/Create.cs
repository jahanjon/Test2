using Application.ControllerLogic.Admin.CourseTitle.DTOs;
using Application.Core;
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
    public class Create
    {
        public class Command:IRequest<Result<Unit>>
        {
            public CreateDto Dto { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _db;
            public Handler(DataContext context)
            {
                _db = context;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = new Domain.CourseTitle
                {
                    Title=request.Dto.Title,
                    Description=request.Dto.Description,
                    CourseId=request.Dto.CourseId,
                };
                var isExsit = await _db.Courses.AnyAsync(x => x.Id == entity.CourseId);
                if(!isExsit)
                {
                    return Result<Unit>.Failure(-400, "شناسه دوره یافت نشد ");
                }
                _db.CourseTitle.Add(entity);
                await _db.SaveChangesAsync();
                return Result<Unit>.Success(200, "سرفصل دوره ها با موفقیت انجام شد ");
            }
        }
    }
}
