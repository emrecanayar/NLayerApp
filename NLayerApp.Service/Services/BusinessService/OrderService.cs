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
    public class OrderService : Service<Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderService(IGenericRepository<Order> repository, IUnitOfWork unitOfWork, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository) : base(repository, unitOfWork)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task CreateOrder(OrderCreateRequestDto orderCreateRequestDto)
        {
            using var transaction = await UnitOfWork.BeginTransactionAsync();
            List<ProductReadResponseDto> productReadResponseDtos = new List<ProductReadResponseDto>();
            productReadResponseDtos.Add(new ProductReadResponseDto
            {
                Id = 1,
                Amount = 5,
                Stock = 3,
                Name = "test",
                Price = 10

            });
            productReadResponseDtos.Add(new ProductReadResponseDto
            {
                Id = 2,
                Amount = 7,
                Stock = 8,
                Name = "test",
                Price = 11

            });
            await transaction.CommitAsync();
            Order addedOrder = await _orderRepository.AddAsync(new Order { OrderDate = orderCreateRequestDto.OrderDate });
            int lastOrderId = _orderRepository.GetLastOrderId() + 1;
            foreach (ProductReadResponseDto product in productReadResponseDtos)
            {
                await _orderDetailRepository.AddAsync(new OrderDetail
                {
                    OrderId = lastOrderId,
                    ProductId = product.Id,
                    Quantity = Convert.ToInt16(product.Amount),
                    UnitPrice = product.Price,
                    Discount = 2
                });
            }
            await UnitOfWork.SaveAsync();
        }
    }
}
