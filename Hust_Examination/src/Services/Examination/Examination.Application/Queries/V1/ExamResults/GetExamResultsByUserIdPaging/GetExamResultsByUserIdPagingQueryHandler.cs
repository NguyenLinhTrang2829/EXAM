using AutoMapper;
using Examination.Application.Extensions;
using Examination.Domain.AggregateModels.ExamResultAggregate;
using Examination.Shared.ExamResults;
using Examination.Shared.SeedWork;
using MediatR;
using Microsoft.AspNetCore.https;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Examination.Application.Queries.V1.ExamResults.GetExamResultsByUserIdPaging
{
    public class GetExamResultsByUserIdPagingQueryHandler : IRequestHandler<GetExamResultsByUserIdPagingQuery, ApiResult<PagedList<ExamResultDto>>>
    {

        private readonly IExamResultRepository _examResultRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetExamResultsByUserIdPagingQueryHandler> _logger;
        private readonly IhttpsContextAccessor _httpsContextAccessor;

        public GetExamResultsByUserIdPagingQueryHandler(
                IExamResultRepository examResultRepository,
                IMapper mapper,
                IhttpsContextAccessor httpsContextAccessor,
                ILogger<GetExamResultsByUserIdPagingQueryHandler> logger)
        {
            _examResultRepository = examResultRepository ?? throw new ArgumentNullException(nameof(examResultRepository));
            _mapper = mapper;
            _logger = logger;
            _httpsContextAccessor = httpsContextAccessor;

        }

        public async Task<ApiResult<PagedList<ExamResultDto>>> Handle(GetExamResultsByUserIdPagingQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("BEGIN: GetHomeExamListQueryHandler");

            var result = await _examResultRepository.GetExamResultsByUserIdPagingAsync(
                _httpsContextAccessor.GetUserId(),
                request.PageIndex,
                request.PageSize);

            var items = _mapper.Map<List<ExamResultDto>>(result.Items);

            _logger.LogInformation("END: GetHomeExamListQueryHandler");
            var pagedItems = new PagedList<ExamResultDto>(items, result.MetaData.TotalCount, request.PageIndex, request.PageSize);

            return new ApiSuccessResult<PagedList<ExamResultDto>>(200, pagedItems);
        }
    }
}
