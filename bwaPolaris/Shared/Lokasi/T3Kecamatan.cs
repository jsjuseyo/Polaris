using System.ComponentModel.DataAnnotations;

namespace bwaPolaris.Shared;

public class T3Kecamatan : BaseModel
{
    [Key]
    [PrimaryKey]
    public long IdKecamatan { get; set; }
    public long? IdKota { get; set; }
    [MaxLength(10)]
    public string? KodeBPS { get; set; }
    [MaxLength(50)]
    public string? Kecamatan { get; set; }
    [MaxLength(20)]
    public string? Inisial { get; set; }
}
