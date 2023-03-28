using Application.Core;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>//Specifying Mediator Unit class to signal that we are not returning anything since commands should not 
        {
            public Activity Activity { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator() { 
                RuleFor(x=> x.Activity).SetValidator(new ActivityValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly ReactivitiesDbContext _context;

            public Handler(ReactivitiesDbContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Activities.Add(request.Activity);
                var result = await _context.SaveChangesAsync() > 0; // Returns the number of entries saved
                
                if (!result) return Result<Unit>.Failure("Failed to create an activity");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
