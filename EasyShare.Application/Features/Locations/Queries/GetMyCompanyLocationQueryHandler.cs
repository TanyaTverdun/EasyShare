using AutoMapper;
using AutoMapper.QueryableExtensions;
using EasyShare.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Locations.Queries;

public class GetMyCompanyLocationQueryHandler 
    : IRequestHandler<GetMyCompanyLocationQuery, LocationDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;
    private readonly IMapper _mapper;

    public GetMyCompanyLocationQueryHandler(
        IApplicationDbContext context, 
        IUserContext userContext,
        IMapper mapper)
    {
        this._context = context;
        this._userContext = userContext;
        this._mapper = mapper;
    }

    public async Task<LocationDto?> Handle(
        GetMyCompanyLocationQuery request, 
        CancellationToken cancellationToken)
    {
        var companyId = this._userContext.UserId;

        return await this._context.Companies
        .Where(c => c.Id == companyId)
        .Select(c => c.Location) 
        .ProjectTo<LocationDto>(this._mapper.ConfigurationProvider)
        .FirstOrDefaultAsync(cancellationToken);
    }
}
