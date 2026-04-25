using AutoMapper;
using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace EasyShare.Application.Features.Users.Queries.GetProfile;

public class GetUserProfileQueryHandler 
    : IRequestHandler<GetUserProfileQuery, UserProfileDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;
    private readonly IMapper _mapper;

    public GetUserProfileQueryHandler(
        IApplicationDbContext context,
        IUserContext userContext,
        IMapper mapper)
    {
        this._context = context;
        this._userContext = userContext;
        this._mapper = mapper;
    }

    public async Task<UserProfileDto> Handle(
        GetUserProfileQuery request, 
        CancellationToken cancellationToken)
    {
        var userId = this._userContext.UserId;

        var user = await _context.Users
            .Include(u => u.Location)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => 
                u.Id == userId, 
                cancellationToken);

        if (user == null)
        {
            throw new NotFoundException("Користувача не знайдено.");
        }

        return this._mapper.Map<UserProfileDto>(user);
    }
}
