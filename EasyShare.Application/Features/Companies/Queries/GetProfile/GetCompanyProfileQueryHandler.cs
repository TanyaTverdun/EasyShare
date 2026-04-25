using AutoMapper;
using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Companies.Queries.GetProfile;

public class GetCompanyProfileQueryHandler 
    : IRequestHandler<GetCompanyProfileQuery, CompanyProfileDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;
    private readonly IMapper _mapper;

    public GetCompanyProfileQueryHandler(
        IApplicationDbContext context, 
        IUserContext userContext, 
        IMapper mapper)
    {
        this._context = context;
        this._userContext = userContext;
        this._mapper = mapper;
    }

    public async Task<CompanyProfileDto> Handle(
        GetCompanyProfileQuery request, 
        CancellationToken ct)
    {
        var companyId = this._userContext.UserId;

        var company = await this._context.Companies
            .Include(c => c.Location)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == companyId, ct);

        if (company == null)
        {
            throw new NotFoundException("Компанію не знайдено.");
        }

        return this._mapper.Map<CompanyProfileDto>(company);
    }
}
