﻿using ECommerce.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.Abstract
{
    public interface IProductDal:IGenericDal<Product> 
    {
        Task<List<Product>> GetProductsWitCategoryAsync();
        IQueryable<Product> GetProductsByCategoryId(int categoryId);

    }
}
