using AutoMapper;
using ITicket.uz.Application.Commons.Interfaces;
using ITicket.uz.Domain.Entities;
using MediatR;
namespace ITicket.uz.Application.UseCases.Rows.Commands.CreateRow
{
    public class CreateRowCommand:IRequest<Guid>
    {
        public Guid VenueId { get; set; }
        public int Number { get; set; }
        public int SeatsEachRow { get; set; }
    }
    public class CreateRowCommandHandler:IRequestHandler<CreateRowCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        private readonly IMapper _mapper;
        public CreateRowCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }
        public async Task<Guid> Handle(CreateRowCommand request, CancellationToken cancellationToken)
        {
            Row row = _mapper.Map<Row>(request);
            await _context.Rows.AddAsync(row);  
            await _context.SaveChangesAsync();
            return row.Id;
        }

    }
}