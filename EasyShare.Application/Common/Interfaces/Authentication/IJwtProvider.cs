using EasyShare.Domain.Enums;

namespace EasyShare.Application.Common.Interfaces.Authentication;

public interface IJwtProvider
{
    string GenerateToken(
        string userId, 
        string email, 
        string name,
        AccountType accountType);
}