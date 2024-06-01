global using bwaPolaris.Data;
global using Grpc.Core;
global using Microsoft.EntityFrameworkCore;

namespace bwaPolaris.Server
{
    public class svsForm: svpMasterForm.svpMasterFormBase
    {
        private IDbContext _db;
        private readonly svsBaseService _bases;
        public svsForm()
        {
            _db = ServerDbContext;
            _bases = new svsBaseService(_db);
        }

        public override async Task<RplForm> GetForm(RqsForm request, ServerCallContext context)
        {
            var dtForm = _db.Set<T0Form>().Where(x => x.Status == true).ToList();
            var hasil = new RplForm();
            hasil.ListT0Form.AddRange(dtForm.Adapt<IEnumerable<RplFormById>>());
            return hasil;
        }

        public override async Task<RplForm> GetFormByIdUser(RqsFormByIdUser request, ServerCallContext context)
        {
            var dtPrivileges = await _db.Set<T9Privileges>().Where(x => x.IdUser.ToString() == request.IdUser && x.IsAbleToReadData == true).ToListAsync();
            var dtForm = new List<T0Form>();
            foreach (var item in dtPrivileges)
            {
                var form = await _db.Set<T0Form>().FirstOrDefaultAsync(x => x.IdForm == item.IdForm);
                dtForm.Add(form);

            }
            var hasil = new RplForm();
            hasil.ListT0Form.AddRange(dtForm.Adapt<IEnumerable<RplFormById>>());
            return hasil;
        }

        public override async Task<RplT1AtributFormById> GetT1AtributFormById(RqsFormById request, ServerCallContext context)
        {
            var data = await _db.Set<T1AtributForm>().Where(x => x.IdForm == request.IdForm).ToListAsync();
            var hasil = new RplT1AtributFormById();
            hasil.ListT1AtributForm.AddRange(data.Adapt<IEnumerable<PtmT1AtributForm>>());
            return hasil;
        }

        public override async Task<RplWriteForm> UpdateAtributForm(PtmT1AtributForm request, ServerCallContext context)
        {
            var data = await _bases.UpdateHeader<T1AtributForm>(request.Adapt<T1AtributForm>(), "Auto");
            await _db.SaveChangesAsync();

            return new RplWriteForm { IsOK = true, Result = "Berhasil" };
        }
    }
}
