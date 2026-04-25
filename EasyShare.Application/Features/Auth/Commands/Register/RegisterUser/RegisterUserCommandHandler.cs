using AutoMapper;
using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using EasyShare.Application.Common.Interfaces.Authentication;
using EasyShare.Application.Features.Auth.Queries.Login;
using EasyShare.Domain.Entities;
using EasyShare.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Auth.Commands.Register.RegisterUser;

public class RegisterUserCommandHandler 
    : IRequestHandler<RegisterUserCommand, AuthResponseDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    private readonly IMapper _mapper;

    public RegisterUserCommandHandler(
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
        RegisterUserCommand request, 
        CancellationToken cancellationToken)
    {
        var emailExists = 
            await this._context.Users
                .AnyAsync(u => u.Email == request.Email, cancellationToken) ||
            await this._context.Companies
                .AnyAsync(c => c.Email == request.Email, cancellationToken);

        if (emailExists)
        {
            throw new ConflictException("Цей Email вже використовується іншим акаунтом.");
        }

        var user = User.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            this._passwordHasher.HashPassword(request.Password));

        this._context.Users.Add(user);
        await this._context.SaveChangesAsync(cancellationToken);

        var token = this._jwtProvider.GenerateToken(
            user.Id.ToString(), 
            user.Email, 
            user.FirstName,
            AccountType.User);

        return this._mapper.Map<AuthResponseDto>(user) with 
        { 
            Token = token 
        };
    }
}