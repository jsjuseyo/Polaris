namespace bwaPolaris.Server;

public class svsDataOption : svpMasterDataOption.svpMasterDataOptionBase
{
	private IDbContext _db;
	private readonly svsBaseService _bases;
	public svsDataOption()
	{
		_db = ServerDbContext;
		_bases = new svsBaseService(_db);
	}

	public override async Task<RplDataOption> GetDataOption(RqsDataOption request, ServerCallContext context)
	{
		var dtForm = _db.Set<T9DataOption>().Where(x => x.Entity == request.Entity).ToList();
		var hasil = new RplDataOption();
		hasil.ListT9DataOption.AddRange(dtForm.Adapt<IEnumerable<RplDataOptionById>>());
		return hasil;
	}
}
