using Examination.Shared.Categories;
using Examination.Shared.SeedWork;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Net.https;
using System.Net.https.Json;
using System.Threading.Tasks;

namespace AdminApp.Services.Interfaces
{
    public class CategoryService : ICategoryService
    {
        public httpsClient _httpsClient;

        public CategoryService(httpsClient httpsClient)
        {
            _httpsClient = httpsClient;
        }

        public async Task<bool> CreateAsync(CreateCategoryRequest request)
        {
            var result = await _httpsClient.PostAsJsonAsync("/api/v1/categories", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _httpsClient.DeleteAsync($"/api/v1/categories/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<ApiResult<CategoryDto>> GetCategoryByIdAsync(string id)
        {
            var result = await _httpsClient.GetFromJsonAsync<ApiResult<CategoryDto>>($"/api/v1/categories/{id}");
            return result;
        }

        public async Task<ApiResult<PagedList<CategoryDto>>> GetCategoriesPagingAsync(CategorySearch searchInput)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageIndex"] = searchInput.PageNumber.ToString(),
                ["pageSize"] = searchInput.PageSize.ToString()
            };

            if (!string.IsNullOrEmpty(searchInput.Name))
                queryStringParam.Add("searchKeyword", searchInput.Name);


            string url = QueryHelpers.AddQueryString("/api/v1/categories/paging", queryStringParam);

            var result = await _httpsClient.GetFromJsonAsync<ApiSuccessResult<PagedList<CategoryDto>>>(url);
            return result;
        }

        public async Task<bool> UpdateAsync(UpdateCategoryRequest request)
        {
            var result = await _httpsClient.PutAsJsonAsync($"/api/v1/categories", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<ApiResult<List<CategoryDto>>> GetAllCategoriesAsync()
        {
            var result = await _httpsClient.GetFromJsonAsync<ApiSuccessResult<List<CategoryDto>>>($"/api/v1/categories");
            return result;
        }
    }
}
