using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShare.Application.Features.ItemTypes.Queries.GetTypesByCategory
{
    public record ItemTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
