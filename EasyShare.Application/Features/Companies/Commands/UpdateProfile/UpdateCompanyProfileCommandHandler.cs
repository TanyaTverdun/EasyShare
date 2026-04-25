using AutoMapper;
using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using EasyShare.Application.Common.Interfaces.Authentication;
using EasyShare.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Companies.Commands.UpdateProfile;

public class UpdateCompanyProfileCommandHandler
: IRequestHandler<UpdateCompanyProfileCommand, CompanyProfileResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;
    private readonly IJwtProvider _jwtProvider;
    private readonly IMapper _mapper;

    public UpdateCompanyProfileCommandHandler(
        IApplicationDbContext context,
        IUserContext userContext,
        IJwtProvider jwtProvider,
        IMapper mapper)
    {
        this._context = context;
        this._userContext = userContext;
        this._jwtProvider = jwtProvider;
        this._mapper = mapper;
    }

    public async Task<CompanyProfileResponse> Handle(
        UpdateCompanyProfileCommand request,
        CancellationToken cancellationToken)
    {
        var companyId = this._userContext.UserId;

        var company = await _context.Companies
            .Include(c => c.Location)
            .FirstOrDefaultAsync
                (c => c.Id == companyId, 
                cancellationToken);

        if (company == null)
        {
            throw new NotFoundException("Компанію не знайдено.");
        }

        if (company.Email != request.Email &&
            await this._context.Companies.AnyAsync(
                c => c.Email == request.Email, 
                cancellationToken))
        {
            throw new ConflictException("Компанія із такою поштою вже існує.");
        }

        this._mapper.Map(request, company);

        await this._context.SaveChangesAsync(cancellationToken);

        var newToken = this._jwtProvider.GenerateToken(
            company.Id.ToString(),
            company.Email,
            company.Name,
            AccountType.Company);

        return this._mapper.Map<CompanyProfileResponse>(company) with
        {
            Token = newToken
        };
    }
}
