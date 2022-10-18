using NLayerApp.Core.Entities.BusinessEntities;
using NLayerApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Dtos.Responses
{
    public class OrderDetailCreateResponseDto : IDto
    {
        public List<OrderDetail> OrderDetail { get; set; }
    }
}
