using Common.Constants;
using Common.Dto;
using Common.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TA.Data.Contracts.Dto;
using TA.Data.Contracts.Entities;
using TA.Data.Repositories;
using TA.RestClients;

namespace TA.Services
{
    public interface ITestAssignerService
    {
        public Task AssignTest(AssignTestRequestDto dto);
        public Task<PaginatedResponse<AssisgnedTestResponseDto>> GetAssignedTests(string token, PaginationRequest request);
    }

    public class TestAssignerService : ITestAssignerService
    {
        private readonly IAssignedTestsRepository _repository;
        private readonly ITAClient _client;

        public TestAssignerService(IAssignedTestsRepository repository, ITAClient client)
        {
            _repository = repository;
            _client = client;
        }

        public async Task AssignTest(AssignTestRequestDto dto)
        {
            await _repository.AssignTest(dto);
        }

        public async Task<PaginatedResponse<AssisgnedTestResponseDto>> GetAssignedTests(string token, PaginationRequest request)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtSecurityToken = handler.ReadJwtToken(token);
            Claim userIdClaim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == JwtClaimsConstants.CLaimUserId)
                ?? throw new AuthException("no user claim in token");

            Guid userId = Guid.Parse(userIdClaim.Value);

            List<Guid>? res = await _client.GetGroupsForUser(userId);

            return await _repository.GetAssignedTests(res, userId, request);
        }
    }
}
