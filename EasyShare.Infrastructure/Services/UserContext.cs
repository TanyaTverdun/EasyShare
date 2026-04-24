using System.Security.Claims;
using EasyShare.Application.Common.Interfaces;
using EasyShare.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace EasyShare.Infrastructure.Services;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        this._httpContextAccessor = httpContextAccessor;
    }

    public int UserId
    {
        get
        {
            var idClaim = this._httpContextAccessor.HttpContext?.User?
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(idClaim) || !int.TryParse(idClaim, out var id))
            {
                throw new UnauthorizedException("Користувач не авторизований.");
            }

            return id;
        }
    }

    public bool IsAuthenticated => this._httpContextAccessor
        .HttpContext?.User?.Identity?.IsAuthenticated ?? false;
}
