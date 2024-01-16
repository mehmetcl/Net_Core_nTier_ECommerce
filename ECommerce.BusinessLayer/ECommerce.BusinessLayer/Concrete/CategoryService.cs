using AutoMapper;
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
    public class CategoryService : GenericService<Category>, ICategoryService
    {
        private readonly ICategoryDal _categorydal;
        private readonly IMapper _mapper;
        public CategoryService(IGenericDal<Category> genericDal, IUnitOfWork unitofWork, ICategoryDal categorydal, IMapper mapper) : base(genericDal, unitofWork)
        {
            _categorydal = categorydal;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<CategoryWithProductDto>> GetSingleCategoryByIdWithProductAsync(int categoryid)
        {
            Category category = await _categorydal.GetSingleCategoryByIdProductsAsync(categoryid);
            var categoryDto = _mapper.Map<CategoryWithProductDto>(category);

            return CustomResponseDto<CategoryWithProductDto>.Success(200, categoryDto);
        }
    }
    }
