global using PrimaryKeyAttribute = SQLite.PrimaryKeyAttribute;

namespace bwaPolaris.Shared;

public class T2Kota : BaseModel
{
    [Key]
    [PrimaryKey]
    public long IdKota { get; set; }
    public long? IdProvinsi { get; set; }
    public long? IdSektorKota { get; set; }
    [MaxLength(10)]
    public string? KodeBPS { get; set; }
    public string? Lambang { get; set; }
    [MaxLength(50)]
    public string? Jenis { get; set; }
    [MaxLength(50)]
    public string? Kota { get; set; }
    [MaxLength(20)]
    public string? Inisial { get; set; }
    public string? Provinsi { get; set; }
    public string? Negara { get; set; }
    public string? Continent { get; set; }
    public string? Region { get; set; }
}
