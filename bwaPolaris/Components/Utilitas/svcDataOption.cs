using static bwaPolaris.Shared.svpMasterDataOption;

namespace bwaPolaris.Components;

public class svcDataOption
{
	private svpMasterDataOptionClient _client { get; set; }
	private svcBaseService _bases { get; set; }
	public svcDataOption(svcBaseService bases)
	{
		_bases = bases;
		_client = new svpMasterDataOptionClient(ClientChannel);
	}

	public async Task<List<T9DataOption>> GetDataOption(string entity)
	{
		var reply = _client.GetDataOption(new RqsDataOption { Entity = entity}, Headers);
		return reply.ListT9DataOption.Adapt<List<T9DataOption>>();
	}
}
