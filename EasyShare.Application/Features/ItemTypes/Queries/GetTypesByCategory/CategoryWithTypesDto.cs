using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShare.Application.Features.ItemTypes.Queries.GetTypesByCategory
{
    public record CategoryWithTypesDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<ItemTypeDto> Types { get; set; }
    }
}
