using MassTransit;

namespace bwaPolaris.Shared
{
    public class T10KonfigurasiAkses : BaseModel
    {
        [Key]
        [SQLite.PrimaryKey]
        public Guid IdKonfigurasiAkses { get; set; } = NewId.NextGuid();
        public string? IdUser { get; set; }
        public string? IdForm { get; set; }
        public bool CanReadData { get; set; }
        public bool CanTambahData { get; set; }
        public bool CanEditData { get; set; }
        public bool CanHapusData { get; set; }

    }
}
