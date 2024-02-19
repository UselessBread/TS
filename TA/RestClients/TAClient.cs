using Common.Exceptions;
using IdentityService.Data.Contracts.DTO;
using RestSharp;

namespace TA.RestClients
{
    public interface ITAClient
    {
        public Task<List<Guid>?> GetGroupsForUser(Guid userId);
        public Task<GetGroupInfoResponseDto> GetGroupInfoById(Guid immutableId);
    }

    public class TAClient : ITAClient
    {
        private readonly RestClient _client;

        public TAClient(IConfiguration config)
        {
            _client = new RestClient(config["Urls:Identity"]);
        }

        public async Task<List<Guid>?> GetGroupsForUser(Guid userId)
        {
            RestRequest request = new RestRequest("identity/getgroupsforuser");
            request.AddQueryParameter("userId", userId);

            return await _client.GetAsync<List<Guid>>(request);
        }

        public async Task<GetGroupInfoResponseDto> GetGroupInfoById(Guid immutableId)
        {
            RestRequest request = new RestRequest("identity/getgroupinfobyid");
            request.AddQueryParameter("immutableId", immutableId);

            var res = await _client.GetAsync<GetGroupInfoResponseDto>(request)
                ?? throw new BadRequestException("Request returned no results");

            return res;
        }
    }
}
