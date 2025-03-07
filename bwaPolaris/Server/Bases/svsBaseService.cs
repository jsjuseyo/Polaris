using System.Reflection;
using static bwaPolaris.Server.modAes;

namespace bwaPolaris.Server
{
    public class svsBaseService
    {
        private readonly IDbContext _db;
        public svsBaseService(IDbContext db)
        {
            _db = db;
        }

        #region Header
        public async Task<T> InsertHeader<T>(T data, string Operator) where T : class
        {
            PropertyInfo[] properties = data.GetType().GetProperties();
            properties.FirstOrDefault(p => p.Name == "Creator")?.SetValue(data, Operator);
            properties.FirstOrDefault(p => p.Name == "Synchronise")?.SetValue(data, "Inserted");
            properties.FirstOrDefault(p => p.Name == "WaktuInsert")?.SetValue(data, DateTime.Now);

            
            if (typeof(T) == typeof(T1Staf))
            {
                var user = await GenerateUser<T1Staf>(data as T1Staf, Operator);
                properties.FirstOrDefault(p => p.Name == "Password")?.SetValue(data, user.Password);
                properties.FirstOrDefault(p => p.Name == "Kode")?.SetValue(data, user.IdUser.ToString());
                await GeneratePrivileges(user.IdUser, "Staf");
            }
            _db.Set<T>().Add(data);

            return data;
        }

        public async Task<T> UpdateHeader<T>(T data, string Operator) where T : class
        {
            PropertyInfo[] properties = data.GetType().GetProperties();
            properties.FirstOrDefault(p => p.Name == "Operator")?.SetValue(data, Operator);
            properties.FirstOrDefault(p => p.Name == "Synchronise")?.SetValue(data, "Updated");
            properties.FirstOrDefault(p => p.Name == "WaktuProses")?.SetValue(data, DateTime.Now);

            if (typeof(T) == typeof(T1Staf))
            {
                var staf = data as T1Staf;
                var user = await _db.Set<T9User>().FirstOrDefaultAsync(x => x.Keterangan == staf.IdStaf.ToString());
                user.Email = staf.Email;
                user.Nama = staf.NamaPanggilan;
                _db.Set<T9User>().Update(user);
            }
            
            _db.Set<T>().Update(data);

            return data;
        }

        public async Task DeleteHeader<T>(T data) where T : class
        {
            if (typeof(T) == typeof(T1Staf))
            {
                var staf = data as T1Staf;
                var user = await _db.Set<T9User>().FirstOrDefaultAsync(x => x.Keterangan == staf.IdStaf.ToString());
                var config = await _db.Set<T9Privileges>().Where(x => x.IdUser == user.IdUser).ToListAsync();
                var jabatanstaf = await _db.Set<T5Jabatan_Staf>().Where(x => x.IdStaf == staf.IdStaf).ToListAsync();
                _db.Set<T9User>().Remove(user);
                _db.Set<T9Privileges>().RemoveRange(config);
                _db.Set<T5Jabatan_Staf>().RemoveRange(jabatanstaf);
            }

            _db.Set<T>().Remove(data);
        }
        #endregion

        #region Detil
        public async Task InsertDetil<T>(IEnumerable<T> entity, string Operator) where T : class
        {

            foreach (var data in entity)
            {
                PropertyInfo[] properties = data.GetType().GetProperties();
                properties.FirstOrDefault(p => p.Name == "Creator")?.SetValue(data, Operator);
                properties.FirstOrDefault(p => p.Name == "Synchronise")?.SetValue(data, "Inserted");
                properties.FirstOrDefault(p => p.Name == "WaktuInsert")?.SetValue(data, DateTime.Now);
            }

            _db.Set<T>().AddRange(entity);
        }

        public async Task UpdateDetil<T>(IEnumerable<T> entity, string Operator, string namaPK, string namaFK, string nilaiFK) where T : class
        {
            try
            {
                List<T> dataDb = await _db.Set<T>().ToListAsync(); // Ambil semua data dari database
                List<T> dataLama = dataDb.Where(x => x.GetType().GetProperty(namaFK).GetValue(x).ToString() == nilaiFK).ToList(); // Ambil semua data dari database
                List<T> dataBaru = entity.ToList(); // Convert data dari client menjadi list

                foreach (var data in dataBaru)
                {
                    PropertyInfo[] properties = data.GetType().GetProperties();
                    properties.FirstOrDefault(p => p.Name == "Operator")?.SetValue(data, Operator);
                    properties.FirstOrDefault(p => p.Name == "Synchronise")?.SetValue(data, "Updated");
                    properties.FirstOrDefault(p => p.Name == "WaktuProses")?.SetValue(data, DateTime.Now);

                    var existing = dataLama.FirstOrDefault(e => e.GetType().GetProperty(namaPK).GetValue(e).ToString() == data.GetType().GetProperty(namaPK).GetValue(data).ToString()); // Misalkan Id adalah property unik untuk setiap data
                    if (existing == null)
                    {
						var cekSemuaData = dataDb.FirstOrDefault(e => e.GetType().GetProperty(namaPK).GetValue(e).ToString() == data.GetType().GetProperty(namaPK).GetValue(data).ToString()); // Misalkan Id adalah property unik untuk setiap data
                        if (cekSemuaData == null) _db.Set<T>().Add(data);
                    }
                    else
                    {
                        // Data ditemukan di database, lakukan operasi update
                        _db.Set<T>().Entry(existing).CurrentValues.SetValues(data);
                    }
                }
                if (typeof(T) != typeof(T9Privileges))
                {
                    foreach (var data in dataLama.Except(dataBaru, new GenericComparer<T>(namaPK)))
                    {
                        _db.Set<T>().Remove(data);
                    }
                }
            }
            catch (Exception ex) { var message = ex.Message; }
        }

        #endregion

        #region Generate
        public async Task<T9User> GenerateUser<T>(T data, string Operator) where T : class
        {
            var user = new T9User();
            user.Synchronise = "Inserted";
            user.WaktuInsert = DateTime.Now;
            user.WaktuProses = DateTime.Now;
            user.Operator = Operator;

            if (typeof(T) == typeof(T1Staf))
            {
                user.Keterangan = (data as T1Staf).IdStaf.ToString();
                user.Nama = (data as T1Staf).NamaPanggilan;
                user.Email = (data as T1Staf).Email;
                user.Password = Encrypt((data as T1Staf).NamaPanggilan);
                user.Username = (data as T1Staf).NamaPanggilan;
                user.Role = "Staf";
            }
            
            _db.Set<T9User>().Add(user);

            return user;

        }

        public async Task GeneratePrivileges(Guid idUser, string role)
        {
            try
            {
                var data = new List<T0Form>();
                if (role == "Staf") data = await _db.Set<T0Form>().Where(x => x.Status == true && x.Keterangan.Contains("Staf")).ToListAsync();
                else data = await _db.Set<T0Form>().Where(x => x.Status == true && !x.Keterangan.Contains("Staf")).ToListAsync();


                foreach (var form in data)
                {
                    var item = new T9Privileges
                    {
                        IdUser = idUser,
                        IdForm = form.IdForm.ToString(),
                        IsAbleToReadData = true,
                        IsAbleToAddData = false,
                        IsAbleToEditData = false,
                        IsAbleToDeleteData = false,
                        WaktuInsert = DateTime.Now,
                        Synchronise = "Generated"
                    };
                    _db.Set<T9Privileges>().Add(item);
                }
                //await _db.SaveChangesAsync();
            }
            catch (Exception e) { throw e; }
        }

        public class GenericComparer<T> : IEqualityComparer<T> where T : class
        {
            private readonly Func<T, object> _keySelector;

            public GenericComparer(string keyName)
            {
                _keySelector = x => x.GetType().GetProperty(keyName).GetValue(x);
            }

            public bool Equals(T x, T y)
            {
                return _keySelector(x).Equals(_keySelector(y));
            }

            public int GetHashCode(T obj)
            {
                return _keySelector(obj).GetHashCode();
            }
        }
        #endregion

        public async Task<string> GetOperator(string idUser)
        {
            var data = await _db.Set<T9User>().FirstOrDefaultAsync(x => x.IdUser.ToString() == idUser);
            return data.Nama;
        }


    }
}
