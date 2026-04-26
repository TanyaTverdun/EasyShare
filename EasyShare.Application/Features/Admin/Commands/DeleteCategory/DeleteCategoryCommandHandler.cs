using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShare.Application.Features.Admin.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCategoryCommandHandler(IApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task Handle(
            DeleteCategoryCommand request, 
            CancellationToken cancellationToken)
        {
            var category = await this._context.Categories
                .Include(c => c.ItemTypes)
                    .ThenInclude(it => it.Attributes)
                .FirstOrDefaultAsync(
                    c => c.Id == request.Id, 
                    cancellationToken);

            if (category == null)
            {
                throw new NotFoundException("Категорію не знайдено.");
            }

            category.Delete();

            await this._context.SaveChangesAsync(cancellationToken);
        }
    }
}
