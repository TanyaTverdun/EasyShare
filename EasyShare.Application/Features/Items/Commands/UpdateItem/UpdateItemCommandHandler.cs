using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using EasyShare.Application.Common.Interfaces.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Items.Commands.UpdateItem
{
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserContext _userContext;
        private readonly IFileService _fileService;

        public UpdateItemCommandHandler(
            IApplicationDbContext context,
            IUserContext userContext,
            IFileService fileService)
        {
            this._context = context;
            this._userContext = userContext;
            this._fileService = fileService;
        }

        public async Task<Unit> Handle(
            UpdateItemCommand request, 
            CancellationToken cancellationToken)
        {
            var companyId = this._userContext.UserId;

            var item = await this._context.Items
                .Include(i => i.ItemAttributeValues)
                .FirstOrDefaultAsync(
                    i => i.Id == request.Id, 
                    cancellationToken);

            if (item == null)
            {
                throw new NotFoundException("Товар не знайдено.");
            }

            if (item.CompanyId != companyId)
            {
                throw new ForbiddenAccessException("Ви не маєте прав редагувати цей товар.");
            }

            var location = await this._context.Locations
                .FirstOrDefaultAsync(l =>
                    l.City == request.City &&
                    l.Street == request.Street &&
                    l.Building == request.Building,
                    cancellationToken);

            if (location == null)
            {
                location = Domain.Entities.Location.Create(
                    request.City, 
                    request.Street, 
                    request.Building);

                this._context.Locations.Add(location);
            }

            string? newImageUrl = null;
            if (request.ImageStream != null && !string.IsNullOrEmpty(request.ImageFileName))
            {
                newImageUrl = await this._fileService.SaveImageAsync(
                    request.ImageStream,
                    request.ImageFileName,
                    cancellationToken);
            }

            var oldImageUrl = item.ImageUrl;

            var attributesDict = request.Attributes?
                .ToDictionary(a => a.AttributeId, a => a.Value);

            item.UpdateDetails(
                request.Name, 
                request.Description, 
                request.TypeId, 
                location,
                request.BillingPeriod, 
                request.Price, 
                request.StockQuantity,
                request.MaxRentDays, 
                request.DepositAmount, 
                request.PrepaymentPercent,
                newImageUrl, 
                attributesDict
            );

            await this._context.SaveChangesAsync(cancellationToken);

            if (newImageUrl != null && 
                !string.IsNullOrEmpty(oldImageUrl))
            {
                if (newImageUrl != oldImageUrl)
                {
                    await this._fileService.DeleteImageAsync(
                        oldImageUrl, 
                        cancellationToken);
                }
            }

            return Unit.Value;
        }
    }
}
