using AutoMapper;
using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using EasyShare.Application.Common.Interfaces.Authentication;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Users.Commands.UpdateProfile;

public class UpdateUserProfileCommandHandler 
    : IRequestHandler<UpdateUserProfileCommand, UserProfileResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;
    private readonly IJwtProvider _jwtProvider;
    private readonly IMapper _mapper;

    public UpdateUserProfileCommandHandler(
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

    public async Task<UserProfileResponse> Handle(
        UpdateUserProfileCommand request, 
        CancellationToken cancellationToken)
    {
        var userId = this._userContext.UserId;

        var user = await _context.Users
            .Include(u => u.Location)
            .FirstOrDefaultAsync(u => 
                u.Id == userId, 
                cancellationToken);

        if (user == null)
        {
            throw new NotFoundException("Користувача не знайдено.");
        }

        if (user.Email != request.Email &&
            await this._context.Users.AnyAsync(u => 
                u.Email == request.Email, 
                cancellationToken))
        {
            throw new ConflictException("Користувач із такою поштою вже існує.");
        }

        this._mapper.Map(request, user);

        await this._context.SaveChangesAsync(cancellationToken);

        var newToken = this._jwtProvider.GenerateToken(
            user.Id.ToString(),
            user.Email,
            user.FirstName);

        return this._mapper.Map<UserProfileResponse>(user) with 
        { 
            Token = newToken 
        };
    }
}
