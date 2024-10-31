using Application.Core;
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
    public class Delete
    {
        public class Command:IRequest<Result<Unit>>
        {
            public int CourseId { get; set; }
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
                var entity = await _db.Courses.FirstOrDefaultAsync(x => x.Id == request.CourseId);
                if (entity == null)
                {
                    return Result<Unit>.Failure(-400, "شناسه مربی  یافت نشد ");
                }
                entity.IsDelete = !entity.IsDelete;
                _db.Courses.Update(entity);
                await _db.SaveChangesAsync();
                return Result<Unit>.Success(200, "مربی با موفقیت حذف شد ");
            }
        }
    }
}
