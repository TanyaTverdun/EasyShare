using AutoMapper;
using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Items.Queries.GetItemById;

public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, ItemDetailsDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetItemByIdQueryHandler(
        IApplicationDbContext context, 
        IMapper mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }

    public async Task<ItemDetailsDto> Handle(
        GetItemByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var item = await this._context.Items
            .AsNoTracking()
            .Include(i => i.Location)
            .Include(i => i.Company)
            .Include(i => i.ItemType)
                .ThenInclude(it => it.Category)
            .Include(i => i.ItemAttributeValues)
                .ThenInclude(iav => iav.Attribute)
            .Include(i => i.Bookings)
                .ThenInclude(b => b.Reviews)
            .Include(i => i.Bookings)
                .ThenInclude(b => b.User)
            .FirstOrDefaultAsync(i => 
                i.Id == request.Id, 
                cancellationToken);

        if (item == null)
        {
            throw new NotFoundException(
                "На жаль, цей товар не знайдено. " +
                "Можливо, його було видалено власником.");
        }

        var attributes = item.ItemAttributeValues
            .ToDictionary(
                iav => iav.Attribute.Name, 
                iav => iav.Value);

        var allReviews = item.Bookings
            .SelectMany(
                b => b.Reviews
                    .Select(
                        r => new 
                        { 
                            Review = r, 
                            b.User 
                        }))
            .ToList();

        double avgRating = allReviews.Count > 0
            ? allReviews
                .Average(x => x.Review.Rating)
            : 0.0;

        return this._mapper.Map<ItemDetailsDto>(item);
    }
}
