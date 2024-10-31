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

namespace Application.ControllerLogic.Admin.CourseTitle
{
    public class List
    {
        public class Query : IRequest<Result<List<Domain.CourseTitle>>>
        {
            public int CourseId { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<List<Domain.CourseTitle>>>
        {
            private readonly DataContext _db;
            public Handler(DataContext context)
            {
                _db = context;
            }
            public async Task<Result<List<Domain.CourseTitle>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var res = await _db.CourseTitle
                    .OrderBy(x => x.Id)
                    .Where(x => !x.Course.IsDelete)
                 .Where(x => x.CourseId == request.CourseId)
                 .ToListAsync();
                return Result<List<Domain.CourseTitle>>.Success(200, "", res);
            }
        }
    }
}
