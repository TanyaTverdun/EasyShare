using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

using DomainAttribute = EasyShare.Domain.Entities.Attribute;

namespace EasyShare.Application.Features.Attributes.Commands.CreateAttribute;

public class CreateAttributeCommandHandler 
    : IRequestHandler<CreateAttributeCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateAttributeCommandHandler(IApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<int> Handle(
        CreateAttributeCommand request, 
        CancellationToken cancellationToken)
    {
        var typeExists = await this._context.ItemTypes
            .AnyAsync(t => 
                t.Id == request.TypeId, 
                cancellationToken);

        if (!typeExists)
        {
            throw new NotFoundException("Тип товару не знайдено.");
        }

        var existing = await this._context.Attributes
            .FirstOrDefaultAsync(a =>
                a.TypeId == request.TypeId &&
                a.Name.ToLower() == request.Name.ToLower(), cancellationToken);

        if (existing != null)
        {
            return existing.Id;
        }

        var attribute = DomainAttribute.Create(request.Name, request.TypeId);

        this._context.Attributes.Add(attribute);
        await this._context.SaveChangesAsync(cancellationToken);

        return attribute.Id;
    }
}
