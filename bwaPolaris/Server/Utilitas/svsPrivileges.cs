namespace bwaPolaris.Server
{
    public class svsPrivileges : svpPrivileges.svpPrivilegesBase
    {
        private IDbContext _db;
        private readonly svsBaseService _bases;
        public svsPrivileges()
        {
            _db = ServerDbContext;
            _bases = new svsBaseService(_db);
        }

        public override async Task<rplPrivileges> GetPrivileges(rqsPrivileges request, ServerCallContext context)
        {
            try
            {
                var user = await _db.Set<T9User>().Where(x => x.IdUser.ToString() == request.IdUser).ToListAsync();

                var data = await (from Privileges in _db.Set<T9Privileges>()
                            join form in _db.Set<T0Form>() on Privileges.IdForm equals form.IdForm
                            where form.HasChild == false && Privileges.IdUser == Guid.Parse(request.IdUser)
                            select new { Privileges = Privileges }).ToListAsync();
                var hasil = new rplPrivileges();
                foreach (var item in data)
                {
                    hasil.DaftarPrivileges.Add(item.Privileges.Adapt<proPrivileges>());
                }
                return hasil;
            }
            catch (Exception e) { throw e; }
        }

        public override async Task<proPrivileges> GetPrivilegesById(rqsPrivileges request, ServerCallContext context)
        {
            try
            {
                var data = await _db.Set<T9Privileges>().FirstOrDefaultAsync(x => x.IdForm.ToString() == request.IdForm && x.IdUser.ToString() == request.IdUser);
                var dataReply = data.Adapt<proPrivileges>();

                return dataReply;
            }
            catch (Exception e) { throw e; }
        }

        public override async Task<rplUpdatePrivileges> InsertPrivileges(proPrivileges request, ServerCallContext context)
        {
            try
            {
                var data = request.Adapt<T9Privileges>();
                await _db.SaveChangesAsync();

                return new rplUpdatePrivileges { Hasil = "OK", IsOK = true };
            }
            catch (Exception e) { throw e; }
        }

        public override async Task<rplUpdatePrivileges> UpdatePrivileges(proPrivileges request, ServerCallContext context)
        {
            try
            {
                var data = request.Adapt<T9Privileges>();
                _db.Set<T9Privileges>().Update(data);
                await _db.SaveChangesAsync();

                return new rplUpdatePrivileges { Hasil = "OK", IsOK = true };
            }
            catch (Exception e) { throw e; }
        }

        public override async Task<rplUpdatePrivileges> DeletePrivileges(proPrivileges request, ServerCallContext context)
        {
            var data = await _db.Set<T9Privileges>().FirstOrDefaultAsync(x => x.IdKonfigurasiAkses.ToString() == request.IdKonfigurasiAkses);
            if (data == null)
            {
                return new rplUpdatePrivileges { Hasil = "Failed", IsOK = false };
            }
            _db.Set<T9Privileges>().Remove(data);
            await _db.SaveChangesAsync();
            return new rplUpdatePrivileges { Hasil = "OK", IsOK = true };
        }

        public override async Task<rplUpdatePrivileges> ResetPrivileges(rqsPrivileges request, ServerCallContext context)
        {
            try
            {
                var data = await _db.Set<T9Privileges>().Where(x => x.IdUser == null).ToListAsync();
                if (data == null)
                {
                    return new rplUpdatePrivileges { Hasil = "Not Found", IsOK = false };
                }
                _db.Set<T9Privileges>().RemoveRange(data);
                await _db.SaveChangesAsync();
                return new rplUpdatePrivileges { Hasil = "OK", IsOK = true };
            }
            catch (Exception e) { throw e; }
        }
    }

}
