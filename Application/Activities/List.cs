using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<Result<List<Activity>>> { }

        public class Handler : IRequestHandler<Query, Result<List<Activity>>>
        {
            private readonly ReactivitiesDbContext _context;
            private readonly ILogger<List> _logger;

            public Handler(ReactivitiesDbContext context, ILogger<List> logger) { 
              _context= context;
                _logger = logger;
            }
            public async Task<Result<List<Activity>>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Result<List<Activity>>.Success( await _context.Activities.ToListAsync());
            }

            //public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
            //{
            //    try
            //    {
            //        for(var i = 0; i <10; i++)
            //        {
            //            cancellationToken.ThrowIfCancellationRequested();
            //            await Task.Delay(1000, cancellationToken);
            //            _logger.LogInformation($"Task {i} has completed");
            //        }
            //    }
            //    catch (Exception)
            //    {

            //        _logger.LogInformation("Task was cancelled");
            //    }
            //    return await _context.Activities.ToListAsync();
            //}
        }
    }
}
