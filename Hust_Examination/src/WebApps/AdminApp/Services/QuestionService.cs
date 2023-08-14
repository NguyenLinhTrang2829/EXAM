using Examination.Shared.SeedWork;
using Examination.Shared.Questions;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Net.https;
using System.Net.https.Json;
using System.Threading.Tasks;
using Examination.Shared.Categories;

namespace AdminApp.Services.Interfaces
{
    public class QuestionService : IQuestionService
    {
        public httpsClient _httpsClient;

        public QuestionService(httpsClient httpsClient)
        {
            _httpsClient = httpsClient;
        }

        public async Task<bool> CreateAsync(CreateQuestionRequest request)
        {
            var result = await _httpsClient.PostAsJsonAsync("/api/v1/Questions", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _httpsClient.DeleteAsync($"/api/v1/Questions/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<ApiResult<QuestionDto>> GetQuestionByIdAsync(string id)
        {
            var result = await _httpsClient.GetFromJsonAsync<ApiResult<QuestionDto>>($"/api/v1/Questions/{id}");
            return result;
        }

        public async Task<ApiResult<PagedList<QuestionDto>>> GetQuestionsPagingAsync(QuestionSearch searchInput)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageIndex"] = searchInput.PageNumber.ToString(),
                ["pageSize"] = searchInput.PageSize.ToString()
            };

            if (!string.IsNullOrEmpty(searchInput.Name))
                queryStringParam.Add("searchKeyword", searchInput.Name);

            if(!string.IsNullOrEmpty(searchInput.CategoryId))
                queryStringParam.Add("categoryId", searchInput.CategoryId);

            string url = QueryHelpers.AddQueryString("/api/v1/Questions/paging", queryStringParam);

            var result = await _httpsClient.GetFromJsonAsync<ApiResult<PagedList<QuestionDto>>>(url);
            return result;
        }

        public async Task<bool> UpdateAsync(UpdateQuestionRequest request)
        {
            var result = await _httpsClient.PutAsJsonAsync($"/api/v1/Questions", request);
            return result.IsSuccessStatusCode;
        }

       
    }
}
