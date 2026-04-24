using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using EasyShare.Application.Common.Interfaces;
using EasyShare.Application.Common.Interfaces.Authentication;
using EasyShare.Application.Common.Exceptions;

namespace EasyShare.Application.Features.Auth.Queries.Login;

public class LoginQueryHandler 
    : IRequestHandler<LoginQuery, AuthResponseDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    private readonly IMapper _mapper;

    public LoginQueryHandler(
        IApplicationDbContext context,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider,
        IMapper mapper)
    {
        this._context = context;
        this._passwordHasher = passwordHasher;
        this._jwtProvider = jwtProvider;
        this._mapper = mapper;
    }

    public async Task<AuthResponseDto> Handle(
        LoginQuery request, 
        CancellationToken cancellationToken)
    {
        var user = await this._context.Users
            .FirstOrDefaultAsync(u => 
                u.Email == request.Email, 
                cancellationToken);

        if (user != null)
        {
            return ProcessAuthentication(
                user,
                request.Password,
                user.PasswordHash,
                user.Id.ToString(),
                user.Email,
                user.FirstName);
        }

        var company = await this._context.Companies
            .FirstOrDefaultAsync(c => 
                c.Email == request.Email, 
                cancellationToken);

        if (company != null)
        {
            return ProcessAuthentication(
                company,
                request.Password,
                company.PasswordHash,
                company.Id.ToString(),
                company.Email,
                company.Name);
        }

        throw new UnauthorizedException("Невірний email або пароль.");
    }

    private AuthResponseDto ProcessAuthentication<T>(
        T account,
        string inputPassword,
        string realHash,
        string id,
        string email,
        string name)
    {
        if (!this._passwordHasher.VerifyPassword(
            inputPassword,
            realHash))
        {
            throw new UnauthorizedException("Невірний email або пароль.");
        }

        var token = this._jwtProvider.GenerateToken(
            id, 
            email, 
            name);

        return this._mapper.Map<AuthResponseDto>(account) with 
        { 
            Token = token 
        };
    }
}