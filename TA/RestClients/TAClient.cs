using RestSharp;

namespace TA.RestClients
{
    public interface ITAClient
    {
        public Task<List<Guid>?> GetGroupsForUser(Guid userId);
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
    }
}
