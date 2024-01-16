using AutoMapper;
using ECommerce.API.Filters;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.EntityLayer.Concrete;
using ECommerce.EntityLayer.DTOS;
using ECommerce.SharedLibrary.Dtos;
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
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoriesDtos = _mapper.Map<List<CategoryDto>>(categories.ToList());
            return CreateActionResult(CustomResponseDto<List<CategoryDto>>.Success(200, categoriesDtos));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto categoryDto)
        {
            var category = await _categoryService.AddAsync(_mapper.Map<Category>(categoryDto));
            var categoriesDto = _mapper.Map<CategoryDto>(category);

            return CreateActionResult(CustomResponseDto<CategoryDto>.Success(201, categoriesDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
            await _categoryService.UpdateAsync(_mapper.Map<Category>(categoryDto));


            return CreateActionResult(SharedLibrary.Dtos.CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "bu Id ye sahip kategori bulunamadı."));


            await _categoryService.RemoveAsync(category);


            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
