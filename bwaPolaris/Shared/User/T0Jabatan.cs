using MassTransit;

namespace bwaPolaris.Shared
{
    public class T0Jabatan : BaseModel
    {
        [Key]
        [PrimaryKey]
        public Guid IdJabatan { get; set; } = NewId.NextGuid();
        [Required]
        public string? Jabatan { get; set; }
        public string? Kode { get; set; }
    }
}
