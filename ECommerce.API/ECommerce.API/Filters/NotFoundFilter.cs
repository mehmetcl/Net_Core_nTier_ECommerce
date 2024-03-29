﻿using ECommerce.BusinessLayer.Abstract;
using ECommerce.EntityLayer.Concrete;
using ECommerce.EntityLayer.DTOS;
using ECommerce.SharedLibrary.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce.API.Filters
{
    public class NotFoundFilter<T>(IGenericService<T> service) : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IGenericService<T> _service = service;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault();
            if (idValue == null) {
                await next.Invoke();
                return;
            }
            var id = (int)idValue;
            var anyEntity=await _service.AnyAsync(x=>x.Id==id);
            if (anyEntity)
            {
                await next.Invoke();
                return;
            }
            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"{typeof(T).Name}({id} not found)",true));
        }
    }
}
