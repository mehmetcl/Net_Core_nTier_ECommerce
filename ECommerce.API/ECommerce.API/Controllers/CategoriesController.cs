using AutoMapper;
using ECommerce.API.Filters;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    
    public class CategoriesController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IGenericService<Category> _service;
        private readonly ICategoryService _categoryService;

        public CategoriesController(IMapper mapper, IGenericService<Category> service, ICategoryService categoryService)
        {
            _mapper = mapper;
            _service = service;
            _categoryService = categoryService;
        }

        [HttpGet("[action]/{CategoryId}")]
        public async Task<IActionResult> GetSingleCategoryByIdWithProductAsync(int CategoryId)
        {
            return CreateActionResult(await _categoryService.GetSingleCategoryByIdWithProductAsync(CategoryId));
        }

    }
}
