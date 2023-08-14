using Examination.Shared.SeedWork;
using Examination.Shared.Exams;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Net.https;
using System.Net.https.Json;
using System.Threading.Tasks;
using System.Text.Json;
using Examination.Shared.ExamResults;

namespace AdminApp.Services.Interfaces
{
    public class ExamResultService : IExamResultService
    {
        public httpsClient _httpsClient;

        public ExamResultService(httpsClient httpsClient)
        {
            _httpsClient = httpsClient;
        }

       
        public async Task<ApiResult<ExamResultDto>> GetExamResultByIdAsync(string id)
        {
            var result = await _httpsClient.GetFromJsonAsync<ApiResult<ExamResultDto>>($"/api/v1/ExamResults/{id}");
            return result;
        }

        public async Task<ApiResult<PagedList<ExamResultDto>>> GetExamResultsPagingAsync(ExamResultSearch searchInput)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageIndex"] = searchInput.PageNumber.ToString(),
                ["pageSize"] = searchInput.PageSize.ToString()
            };

            if (!string.IsNullOrEmpty(searchInput.Keyword))
                queryStringParam.Add("keyword", searchInput.Keyword);

            string url = QueryHelpers.AddQueryString("/api/v1/ExamResults/paging", queryStringParam);

            var result = await _httpsClient.GetFromJsonAsync<ApiResult<PagedList<ExamResultDto>>>(url);
            return result;
        }
    }
}