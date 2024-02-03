using Common.Constants;
using Common.Dto;
using Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TA.Data.Contracts.Dto;
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
        public async Task AssignTest(AssignTestRequestDto dto)
        {
            await _service.AssignTest(dto);
        }

        [Authorize]
        public async Task<PaginatedResponse<AssisgnedTestResponseDto>> GetAssignedTests(PaginationRequest request)
        {
            string initialToken = Request.Headers.Authorization.FirstOrDefault(h => h.StartsWith("Bearer"))
                ?? throw new AuthException("token cannot be empty");
            string resultToken = initialToken.Replace("Bearer", "").Trim();

            return await _service.GetAssignedTests(resultToken, request);
        }
    }
}
