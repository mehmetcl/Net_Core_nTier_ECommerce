﻿using AutoMapper;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Concrete;
using ECommerce.EntityLayer.Concrete;
using ECommerce.EntityLayer.DTOS;
using ECommerce.SharedLibrary.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController(IMapper mapper, IBasketService basketService) : CustomBaseController
    {
        private readonly IMapper _mapper = mapper;
        private readonly IBasketService _basketService = basketService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var baskets = await _basketService.GetAllAsync();
            var basketsDtos = _mapper.Map<List<BasketDto>>(baskets.ToList());
            return CreateActionResult(CustomResponseDto<List<BasketDto>>.Success(200, basketsDtos));
        }
        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> GetBaskets(string userId)
        {
            var baskets = await _basketService.GetBasketsAsync(userId);
            var basketsDtos = _mapper.Map<List<BasketDto>>(baskets.ToList());
            return CreateActionResult(CustomResponseDto<List<BasketDto>>.Success(200, basketsDtos));
        }
        [HttpGet("[action]/{userId}/{productId}")]
        public async Task<IActionResult> GetSepetByUserIdAndProductId(string userId, int productId)
        {
            var baskets = await _basketService.GetBasketByProductIdAndUserIdAsync(userId, productId);
            var basketsDtos = _mapper.Map<BasketDto>(baskets);
            return CreateActionResult(CustomResponseDto<BasketDto>.Success(200, basketsDtos));
        }
        [HttpGet("{id}")] 
        public async Task<IActionResult> GetById(string id)
        {
            var basket = await _basketService.GetByIdAsync(id);
            var basketDto = _mapper.Map<BasketDto>(basket);

            return CreateActionResult(CustomResponseDto<BasketDto
                >.Success(200, basketDto));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddBasket(BasketDto basketDto)
        {
            var basket = await _basketService.AddBasketsAsync(_mapper.Map<Basket>(basketDto));
            var basketsDto = _mapper.Map<BasketDto>(basket);
            return CreateActionResult(CustomResponseDto<BasketDto>.Success(201, basketsDto));
        }
        [HttpPost]
        public async Task<IActionResult> Save(BasketDto basketDto)
        {
            var basket = await _basketService.AddAsync(_mapper.Map<Basket>(basketDto));
            var basketsDto = _mapper.Map<BasketDto>(basket);

            return CreateActionResult(CustomResponseDto<BasketDto>.Success(201, basketsDto));
        }
        [HttpPut]
        public async Task<IActionResult> Update(BasketDto basketDto)
        {
            await _basketService.UpdateAsync(_mapper.Map<Basket>(basketDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, true));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            var basket = await _basketService.GetByIdAsync(id);
            await _basketService.RemoveAsync(basket);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, true));
        }
    }
}
