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
    public class Delete
    {
        public class Command:IRequest<Result<Unit>>
        {
            public int CourseTitleId { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _db;
            public Handler(DataContext context)
            {
                 _db=context;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = await _db.CourseTitle.FirstOrDefaultAsync(x => x.Id == request.CourseTitleId);
                if (entity == null)
                {
                    return Result<Unit>.Failure(-400, "شناسه سرفصل دوره  یافت نشد ");
                }
                _db.CourseTitle.Remove(entity);
                await _db.SaveChangesAsync();
                return Result<Unit>.Success(200, "سرفصل دوره با موفقیت حذف شد ");
            }
        }
    }
}
