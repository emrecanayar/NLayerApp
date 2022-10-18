using AutoMapper;
using NLayerApp.Core.Dtos.Requests;
using NLayerApp.Core.Dtos.Responses;
using NLayerApp.Core.Entities.BusinessEntities;
using NLayerApp.Core.Repositories;
using NLayerApp.Core.Services.BusinessService;
using NLayerApp.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Service.Services.BusinessService
{
    public class OrderDetailService : Service<OrderDetail>, IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailService(IGenericRepository<OrderDetail> repository, IUnitOfWork unitOfWork, IOrderDetailRepository orderDetailRepository) : base(repository, unitOfWork)
        {
            _orderDetailRepository = orderDetailRepository;
        }

    }
}
