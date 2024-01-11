using AutoMapper;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.DataAccessLayer.Abstract;
using ECommerce.DataAccessLayer.UnitOfWork;
using ECommerce.EntityLayer.Concrete;
using ECommerce.EntityLayer.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BusinessLayer.Concrete
{
    public class ProductService : GenericService<Product>, IProductService
    {
        private readonly IProductDal _productdal;
        private readonly IMapper _mapper;

        public ProductService(IGenericDal<Product> genericDal, IUnitOfWork unitofWork, IProductDal productdal, IMapper mapper) : base(genericDal, unitofWork)
        {
            _productdal = productdal;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductWithCategory()
        {
            var product = await _productdal.GetProductWithCategoryAsync();

            var producsDto = _mapper.Map<List<ProductWithCategoryDto>>(product);
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, producsDto);
        }
    }
}






