using Examination.Shared.Categories;
using Examination.Shared.SeedWork;
using Microsoft.AspNetCore.https;
using PortalApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.https;
using System.Threading.Tasks;

namespace PortalApp.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        public CategoryService(IhttpsClientFactory httpsClientFactory,
            IhttpsContextAccessor httpsContextAccessor) : base(httpsClientFactory, httpsContextAccessor)
        {

        }
        public async Task<ApiResult<List<CategoryDto>>> GetAllCategoriesAsync()
        {
            var result = await GetAsync<List<CategoryDto>>("/api/v1/categories");
            return result;
        }
    }
}
