using AutoMapper;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.EntityLayer.Concrete;
using ECommerce.EntityLayer.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public OrderController(IMapper mapper, IOrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllAsync();
            var ordersDtos = _mapper.Map<List<OrderDto>>(orders.ToList());
            return CreateActionResult(CustomResponseDto<List<OrderDto>>.Success(200, ordersDtos.ToList()));
        }
        [HttpPost]
        public async Task<IActionResult> Save(OrderDto orderDto)
        {
            var order = await _orderService.CreateOrderAsync(_mapper.Map<Order>(orderDto));
            var ordersDto = _mapper.Map<OrderDto>(order);
            return CreateActionResult(CustomResponseDto<OrderDto>.Success(201, ordersDto));
        }
        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> GetOrders(int userId)
        {
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            var ordersDtos = _mapper.Map<List<OrderDto>>(orders.ToList());
            return CreateActionResult(CustomResponseDto<List<OrderDto>>.Success(200, ordersDtos));
        }
    }
}
