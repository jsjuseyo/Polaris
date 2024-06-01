using Grpc.Net.Client.Web;
using Grpc.Net.Client;
using static bwaPolaris.Shared.svpLogin;

namespace bwaPolaris.Components;

public class svcLogin
{
	private svpLoginClient _client { get; set; }
	private svcBaseService _bases { get; set; }
	public svcLogin(svcBaseService bases)
	{
		_bases = bases;
		_client = new svpLoginClient(ClientChannel);
	}

	public async Task<RplLogin> OnLogin(string username, string password)
	{
		var request = new RqsLogin { Username = username, Password = password };
		var reply = _client.Login(request, Headers);
		return reply;
	}

	public async Task<T9User> GetUser(string idUser)
	{
		var request = new RqsUser { IdUser = idUser };
		var reply = await _client.GetUserAsync(request, Headers);
		return reply.Adapt<T9User>();
	}

}
