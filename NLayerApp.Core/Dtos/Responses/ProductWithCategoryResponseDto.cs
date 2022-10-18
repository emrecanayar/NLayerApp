using NLayerApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Dtos.Responses
{
    public class ProductWithCategoryResponseDto : ProductReadResponseDto, IDto
    {
        public CategoryReadResponseDto Category { get; set; }
    }
}
