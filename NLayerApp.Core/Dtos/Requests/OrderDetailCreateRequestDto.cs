using NLayerApp.Core.Dtos.Responses;
using NLayerApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Dtos.Requests
{
    public class OrderDetailCreateRequestDto : IDto
    {
        public int OrderId { get; set; }
        public List<ProductReadResponseDto> Products { get; set; }
    }
}
