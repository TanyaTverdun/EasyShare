using AutoMapper;
using AutoMapper.QueryableExtensions;
using EasyShare.Application.Common.Interfaces;
using EasyShare.Application.Features.Companies.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Companies.Queries.GetItems;

public class GetCompanyItemsQueryHandler
: IRequestHandler<GetCompanyItemsQuery, List<CompanyItemDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;
    private readonly IMapper _mapper;

    public GetCompanyItemsQueryHandler(
        IApplicationDbContext context,
        IUserContext userContext,
        IMapper mapper)
    {
        this._context = context;
        this._userContext = userContext;
        this._mapper = mapper;
    }

    public async Task<List<CompanyItemDto>> Handle(
        GetCompanyItemsQuery request,
        CancellationToken cancellationToken)
    {
        var companyId = this._userContext.UserId;

        var query = this._context.Items
            .Where(i => i.CompanyId == companyId)
            .AsNoTracking()
            .Search(request.SearchTerm)
            .FilterByStatus(request.IsActive)
            .Sort(request.SortBy);

        return await query
            .ProjectTo<CompanyItemDto>(this._mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
