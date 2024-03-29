﻿using AutoMapper;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.DataAccessLayer.Abstract;
using ECommerce.DataAccessLayer.UnitOfWork;
using ECommerce.EntityLayer.Concrete;
using ECommerce.EntityLayer.DTOS;
using ECommerce.SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BusinessLayer.Concrete
{
    public class ProductService(IGenericDal<Product> genericDal, IUnitOfWork unitofWork, IProductDal productdal, IMapper mapper) : GenericService<Product>(genericDal, unitofWork), IProductService
    {
        private readonly IProductDal _productdal = productdal;
        private readonly IMapper _mapper = mapper;

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductWithCategory()
        {
            var product = await _productdal.GetProductsWitCategoryAsync();

            var producsDto = _mapper.Map<List<ProductWithCategoryDto>>(product);
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, producsDto);
        }
    }
}






