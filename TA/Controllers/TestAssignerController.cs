using Common.Constants;
using Common.Dto;
using Common.Exceptions;
using Common.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TA.Data.Contracts.Dto;
using TA.Data.Contracts.Entities;
using TA.Services;

namespace TA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestAssignerController : Controller
    {
        private readonly ITestAssignerService _service;

        public TestAssignerController(ITestAssignerService service)
        {
            _service = service;
        }

        [HttpPost("assign")]
        [Authorize(Roles = "Teacher")]
        public async Task AssignTest(AssignTestRequestDto dto)
        {
            Guid userId = JwtTokenHelpers.GetUserIdFromToken(Request);

            await _service.AssignTest(dto, userId);
        }

        [Authorize]
        [HttpPost("getassignedtests")]
        public async Task<PaginatedResponse<AssisgnedTestResponseDto>> GetAssignedTests(PaginationRequest request)
        {
            Guid userId = JwtTokenHelpers.GetUserIdFromToken(Request);

            return await _service.GetAssignedTests(userId, request);
        }

        [Authorize]
        [HttpPost("saveanswers")]
        public async Task SaveAnswers(SaveAnswersDto dto)
        {
            Guid userId = JwtTokenHelpers.GetUserIdFromToken(Request);

            await _service.SaveAnswers(dto, userId);
        }

        [Authorize]
        [HttpPost("reviewdescriptions")]
        public async Task<PaginatedResponse<TestsForReviewResponseDto>> GetTestDescriptionsForReview(PaginationRequest<Guid> paginationRequest)
        {
            return await _service.GetTestDescriptionsForReview(paginationRequest);
        }

        [Authorize]
        [HttpPost("getbyimmutableid")]
        public async Task<AssisgnedTestResponseDto> GetAssignmentByImmutableId(Guid immutableId)
        {
            return await _service.GetAssignmentByImmutableId(immutableId);
        }
    }
}
