﻿using AutoMapper;
using ECommerce.API.Filters;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.EntityLayer.Concrete;
using ECommerce.EntityLayer.DTOS;
using ECommerce.SharedLibrary.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{

    public class ProductsController(IGenericService<Product> service, IMapper mapper, IProductService productService) : CustomBaseController
    {
        private readonly IMapper _mapper = mapper;
        private readonly IGenericService<Product> _service = service;
        private readonly IProductService _productService = productService;

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductWithCategory()
        {
            return  CreateActionResult(await _productService.GetProductWithCategory());
        }


        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }


        [ServiceFilter(typeof(NotFoundFilter<Product>))] 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _service.GetByIdAsync(id);
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productsDto));
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productsDto));
        }
        [HttpPut]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
           await _service.UpdateAsync(_mapper.Map<Product>(productDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, true));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            var product =await _service.GetByIdAsync(id);   
           await _service.RemoveAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204,true));
        }
    }
}
