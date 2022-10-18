using NLayerApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Dtos.Responses
{
    public class CategoryWithProductsResponseDto : CategoryReadResponseDto, IDto
    {
        public List<ProductReadResponseDto> Products { get; set; }
    }
}
