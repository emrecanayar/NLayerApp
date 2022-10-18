using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayerApp.API.Controllers.Base;
using NLayerApp.Core.Dtos.Requests;
using NLayerApp.Core.Dtos.ResponseTypes.Concrete;
using NLayerApp.Core.Services.BusinessService;

namespace NLayerApp.API.Controllers
{
    public class OrdersController : CustomBaseController
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Save(OrderCreateRequestDto orderCreateRequestDto)
        {
            await _orderService.CreateOrder(orderCreateRequestDto);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(201, data: null, isSuccess: true));
        }

    }
}
