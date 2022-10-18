using NLayerApp.Core.Dtos.Requests;
using NLayerApp.Core.Entities.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Services.BusinessService
{
    public interface IOrderService : IService<Order>
    {
        Task CreateOrder(OrderCreateRequestDto orderCreateRequestDto);
    }
}
