using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShare.Application.Features.Attributes.Commands.UpdateAttribute;

public class UpdateAttributeCommandHandler 
    : IRequestHandler<UpdateAttributeCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateAttributeCommandHandler(IApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<Unit> Handle(
        UpdateAttributeCommand request, 
        CancellationToken cancellationToken)
    {
        var attribute = await this._context.Attributes
            .FirstOrDefaultAsync(
                a => a.Id == request.Id && !a.IsDeleted, 
                cancellationToken);

        if (attribute == null)
        {
            throw new NotFoundException("Характеристику не знайдено.");
        }

        var isNameTaken = await this._context.Attributes
            .AnyAsync(a => a.TypeId == attribute.TypeId &&
                           a.Id != request.Id &&
                           !a.IsDeleted &&
                           a.Name.ToLower() == request.Name.ToLower(),
                           cancellationToken);

        if (isNameTaken)
        {
            throw new InvalidOperationException(
                $"Характеристика з назвою '{request.Name}' вже існує для цього типу товару.");
        }

        attribute.UpdateName(request.Name);

        await this._context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
