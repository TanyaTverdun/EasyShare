using MediatR;

namespace EasyShare.Application.Features.Users.Queries.GetProfile;

public record GetUserProfileQuery : IRequest<UserProfileDto>;
