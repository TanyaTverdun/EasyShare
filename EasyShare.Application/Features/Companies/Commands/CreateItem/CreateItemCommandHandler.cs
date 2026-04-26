using EasyShare.Application.Common.Interfaces;
using EasyShare.Application.Common.Interfaces.Services;
using EasyShare.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Companies.Commands.CreateItem;

public class CreateItemCommandHandler 
    : IRequestHandler<CreateItemCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;
    private readonly IFileService _fileService;

    public CreateItemCommandHandler(
        IApplicationDbContext context,
        IUserContext userContext,
        IFileService fileService)
    {
        this._context = context;
        this._userContext = userContext;
        this._fileService = fileService;
    }

    public async Task<int> Handle(
        CreateItemCommand request, 
        CancellationToken cancellationToken)
    {
        var companyId = this._userContext.UserId;

        var location = await this._context.Locations
            .FirstOrDefaultAsync(l =>
                l.City == request.City &&
                l.Street == request.Street &&
                l.Building == request.Building,
                cancellationToken);

        if (location == null)
        {
            location = Location.Create(
                request.City, 
                request.Street, 
                request.Building);

            this._context.Locations.Add(location);
        }

        string? relativeImageUrl = null;
        if (request.ImageStream != null && 
            !string.IsNullOrEmpty(request.ImageFileName))
        {
            relativeImageUrl = await this._fileService.SaveImageAsync(
                request.ImageStream,
                request.ImageFileName,
                cancellationToken);
        }

        var attributesDict = request.Attributes?
            .ToDictionary(a => a.AttributeId, a => a.Value);

        var item = Item.Create(
            request.Name,
            request.Description,
            companyId,
            request.TypeId,
            location,
            request.BillingPeriod,
            request.Price,
            request.StockQuantity,
            request.MaxRentDays,
            request.DepositAmount,
            request.PrepaymentPercent,
            relativeImageUrl,
            attributesDict
        );

        this._context.Items.Add(item);
        await this._context.SaveChangesAsync(cancellationToken);

        return item.Id;
    }
}
